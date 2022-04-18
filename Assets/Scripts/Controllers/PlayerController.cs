using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private HealthBarController healthBarController;

    [SerializeField]
    private GameObject rightHand;

    [SerializeField]
    private GameObject leftHand;

    [SerializeField]
    private GameObject UI;

    [SerializeField]
    private float walkSpeed = 10f;

    [SerializeField]
    private float runSpeed = 20f;

    [SerializeField]
    private float shortJumpSpeed = 300f;

    [SerializeField]
    private float longJumpSpeed = 20f;

    [SerializeField]
    private float turnSmooth = 0.1f;


    private float currentRotation;

    /* ---------------------- CURRENT GAME OBJECT COMPONENTS ---------------------- */

    private Rigidbody rb;
    private Animator animator;
    private Vector3 movementDirection;
    private PlayerStat playerStat;
    
    /* ---------------------------- BOOLEAN VARAIBLES --------------------------- */

    private bool isShortJump;
    private bool isPickup;
    private bool isRunning;
    private bool isWalking;
    private bool isAttacked;
    private bool isNotGrounded;
    private bool isRunHold;

    /* ----------------------- CONTROLERS OF GAME OBJECTS ----------------------- */

    private Collectable collectableInArea;
    private Collectable activeItemInHandRight;
    private Collectable activeItemInHandLeft;
    private UserInterfaceController userInterfaceController;

    void Start() {
        rb = gameObject.GetComponent<Rigidbody>();
        animator = gameObject.GetComponent<Animator>();
        playerStat = gameObject.GetComponent<PlayerStat>();
        userInterfaceController = UI.GetComponent<UserInterfaceController>();
    }

    void Update() {
        if (isShortJump) {
            rb.AddForce(Vector3.up * shortJumpSpeed, ForceMode.Impulse);
            isShortJump = false;
        }
    }

    void FixedUpdate() {
        if (movementDirection == Vector3.zero) {
            if (isWalking) {
                animator.SetBool("Walk", false);
            }

            if (isRunning) {
                animator.SetBool("Run", false);
            }
            return;
        }


        // Smooth character rotation when dirction chagnes

        if (!isPickup || !IsAttacking()) {
            float targetAngle = Mathf.Atan2(movementDirection.x, movementDirection.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref currentRotation, turnSmooth);
            
            rb.MovePosition(rb.position + movementDirection * (isRunning ? runSpeed : walkSpeed) * Time.fixedDeltaTime);
            rb.MoveRotation(Quaternion.Euler(0f, angle, 0f));
        }
    }
   
    /* -------------------------------------------------------------------------- */
    /*                                    INPUTS                                  */
    /* -------------------------------------------------------------------------- */

    void OnMove(InputValue movement) {
        Vector2 movementVector = movement.Get<Vector2>();
        movementDirection = new Vector3(movementVector.x, 0.0f, movementVector.y);

        if (!isRunning) {
            animator.SetBool("Walk", true);
            isWalking = true;
        }

        if (isRunning) {
            animator.SetBool("Run", true);
        }

    } 

    void OnRun() {
        if (movementDirection == Vector3.zero) {
            return;
        }

        isRunHold = !isRunHold;
        if(isRunHold) {
            animator.SetBool("Run", true);
            animator.SetBool("Walk", false);
            isRunning = true;
            isWalking = false;
            return;
        }
        animator.SetBool("Run", false);
        animator.SetBool("Walk", true);
        isRunning = false;
        isWalking = true;
    }

    void OnShortJump() {
        if (!isNotGrounded) {
            isNotGrounded = true;
            isShortJump = true;
            // isWalking = false;
            // isRunning = false;
            animator.SetTrigger("ShortJump");
        }
    }

    void OnShortAttack() {
        if(!IsAttacking()) {
            GameObject activeItem = GetActiveElementInRightHand();

            if (activeItem != null) {
                CollectableTypes collectableType = activeItem.GetComponent<Collectable>().GetCollectableType();

                if (collectableType == CollectableTypes.WHITE_WEAPON || collectableType == CollectableTypes.FIRE_WEAPON) {
                    animator.SetTrigger("ShortAttack");
                    Weapon weapon = activeItem.GetComponent<Weapon>();
                    if (weapon == null) {
                        return;
                    }   
                    StartCoroutine(weapon.Attack());
                }
            }
        }
    }

    void OnHeal() {
        GameObject activeItem = GetActiveElementInLeftHand();
        if (activeItem) {
            CollectableTypes type = activeItem.GetComponent<Collectable>().GetCollectableType();
            if (type == CollectableTypes.POTION) {

                animator.SetTrigger("UsePotion");
                //TODO Potion logic
            }
        }
    }

    public IEnumerator GetHit(int damage) {
        if(!isAttacked && !IsAttacking()) {
            isAttacked = true;
            yield return new WaitForSeconds(0.25f);
            playerStat.GetDamage(damage);
            healthBarController.SetHealth(playerStat.GetHealth());
            if (IsDead()) {
                animator.SetTrigger("Dead");
                // Destroy(this);
                GameStateManager.GetInstance().GameOver();
                yield return null;
            }
            animator.SetTrigger("Hit");
            isAttacked = false;
        }
        
    }

    private bool IsAttacking() {
        if (activeItemInHandRight == null) {
            return false;
        }

        Weapon weapon = activeItemInHandRight.GetComponent<Weapon>();
        if (weapon == null) {
            return false;
        }

        return weapon.GetAttackState();
    }

    private bool IsDead() {
        if (playerStat.GetHealth() <= 0) {
            return true;
        }
        return false;
    }

    void OnPickup() {
        if (collectableInArea != null) {
            isPickup = true;
            userInterfaceController.CloseMessageInfoBox();
            animator.SetTrigger("PickUp");
            Invoke("PickupItemAfterAnimation", 1f);
        }
    }

    void OnDrop() {
        GameObject activeItem = GetActiveElementInRightHand();

        if (activeItem) {
            Rigidbody itemRb = activeItem.AddComponent<Rigidbody>();
            itemRb.AddForce(transform.forward * 2.0f, ForceMode.Impulse);
            
            Invoke("PlaceItemOnGround", 0.25f);
        }
    }


    /* -------------------------------------------------------------------------- */
    /*                               INVOCE METHODS                               */
    /* -------------------------------------------------------------------------- */

    private void PlaceItemOnGround() {
        activeItemInHandRight.GetComponent<BoxCollider>().enabled = true;
        Destroy((activeItemInHandRight as MonoBehaviour).GetComponent<Rigidbody>());
        activeItemInHandRight.transform.parent = null;
        activeItemInHandRight = null;
    }

    private void PickupItemAfterAnimation() {
        collectableInArea.OnPickup();
        if (collectableInArea.GetCollectPlace() == CollectPlaces.RIGHT_HAND) {
            SetItemActive(rightHand, CollectPlaces.RIGHT_HAND);
        }

        if (collectableInArea.GetCollectPlace() == CollectPlaces.LEFT_HAND) {
            SetItemActive(leftHand, CollectPlaces.LEFT_HAND);
        }
        collectableInArea.OnActive();
        isPickup = false;
    }

    private void OnCollisionEnter() {
        if(isNotGrounded) {
            isNotGrounded = false;
            if (isRunning) {
                animator.SetBool("Run", true);
            }

            if (isWalking) {
                animator.SetBool("Walk", true);
            }
        }
    }


    /* -------------------------------------------------------------------------- */
    /*                                  TRIGGERS                                  */
    /* -------------------------------------------------------------------------- */

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Water") {
            KillPlayer();
        }

        if (other.gameObject.tag == "Ground") {
            if (isNotGrounded == true) {
                isNotGrounded = false;
            }
        }

        Collectable collectable = other.GetComponent<Collectable>();

        if(collectable != null && GetActiveElementInRightHand() == null) {
            userInterfaceController.OpenMessageInfoBox(collectable.GetInteractionText());
            collectableInArea = collectable;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (collectableInArea != null) {
            userInterfaceController.CloseMessageInfoBox();
            collectableInArea = null;
        }
    }

    /* -------------------------------------------------------------------------- */
    /*                               HELP FUNCTIONS                               */
    /* -------------------------------------------------------------------------- */


    private void SetItemActive(GameObject hand, CollectPlaces place)
    {
        GameObject activeItem = (collectableInArea as MonoBehaviour).gameObject;
        activeItem.SetActive(true);
        activeItem.transform.parent = hand.transform;
        activeItem.GetComponent<BoxCollider>().enabled = false;

        if (place == CollectPlaces.LEFT_HAND) {
            activeItemInHandLeft = collectableInArea;
            return;
        }
        activeItemInHandRight = collectableInArea;
    }

    private GameObject GetActiveElementInRightHand() {
        if (activeItemInHandRight == null) {
            return null;
        }
        return (activeItemInHandRight as MonoBehaviour).gameObject;
    }

    private GameObject GetActiveElementInLeftHand() {
        return (activeItemInHandLeft as MonoBehaviour).gameObject;
    }

    private void KillPlayer() {
        StartCoroutine(GetHit(playerStat.GetMaxHealth()));
    }
}

