using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using ScriptManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private GameObject rightHand;

    [SerializeField]
    private GameObject leftHand;

    [SerializeField]
    private GameObject UI;

    [SerializeField]
    private int maxHealth = 100;

    [SerializeField]
    private int currentHealth = 100;

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

    /* ---------------------- CURRENT GAME OBJECT ELEMENTS ---------------------- */

    private Rigidbody rb;
    private Animator animator;
    private Vector3 movementDirection;
    
    /* ---------------------------- BOOLEAN VARAIBLES --------------------------- */

    private bool isShortJump;
    private bool isPickup;

    /* ----------------------- CONTROLERS OF GAME OBJECTS ----------------------- */

    private Collectable collectableInArea;
    private Collectable activeItemInHandRight;
    private Collectable activeItemInHandLeft;
    private UserInterfaceController userInterfaceController;

    void Start() {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        userInterfaceController = UI.GetComponent<UserInterfaceController>();
    }

    void Update() {
        if (isShortJump) {
            rb.AddForce(Vector3.up * (isShortJump ? shortJumpSpeed : longJumpSpeed));
            isShortJump = false;
        }

        if (collectableInArea && isPickup) {
            animator.SetTrigger("PickUp");
        }
    }

    void FixedUpdate() {
        bool isRunning = animator.GetBool("Run");
        bool isWalking = animator.GetBool("Walk");

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

        float targetAngle = Mathf.Atan2(movementDirection.x, movementDirection.z) * Mathf.Rad2Deg;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref currentRotation, turnSmooth);
        
        rb.MovePosition(rb.position + movementDirection * (animator.GetBool("Run") ? runSpeed : walkSpeed) * Time.fixedDeltaTime);
        rb.MoveRotation(Quaternion.Euler(0f, angle, 0f));

    }
   
    /* -------------------------------------------------------------------------- */
    /*                                    INPUTS                                  */
    /* -------------------------------------------------------------------------- */

    void OnMove(InputValue movement) {
        Vector2 movementVector = movement.Get<Vector2>();
        movementDirection = new Vector3(movementVector.x, 0.0f, movementVector.y);

        if (movementDirection == Vector3.zero) {
            animator.SetBool("Walk", false);
        } 

        if(!animator.GetBool("Walk") && movementDirection != Vector3.zero) {
            animator.SetBool("Walk", true);
        }
    } 

    void OnRun() {
        bool isRunning = animator.GetBool("Run");
        bool isWalking = animator.GetBool("Walk");

        if (isWalking) {
            animator.SetBool("Run", true);
        }

        if (isRunning) {
            animator.SetBool("Run", false);
        }
    }

    void OnShortJump() {
        isShortJump = true;
        animator.SetTrigger("ShortJump");
    }

    void OnShortAttack() {
        GameObject activeItem = GetActiveElementInRightHand();
        if (activeItem) {
            Collectable collectable = activeItem.GetComponent<Collectable>();

            if (collectable.GetCollectableType() == CollectableTypes.WEAPON) {
                animator.SetTrigger("ShortAttack");
                //TODO Attack Logic
            }

            if (collectable.GetCollectableType() == CollectableTypes.COIN) {
                (collectable as CoinController).Throw(transform);
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

    void OnPickup() {
        if (collectableInArea != null) {
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
    }


    /* -------------------------------------------------------------------------- */
    /*                                  TRIGGERS                                  */
    /* -------------------------------------------------------------------------- */

    private void OnTriggerEnter(Collider other) {
        Collectable collectable = other.GetComponent<Collectable>();

        if(collectable != null) {
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

        if (place == CollectPlaces.LEFT_HAND) {
            activeItemInHandLeft = collectableInArea;
            return;
        }
        activeItemInHandRight = collectableInArea;
    }

    private GameObject GetActiveElementInRightHand() {
        return (activeItemInHandRight as MonoBehaviour).gameObject;
    }

    private GameObject GetActiveElementInLeftHand() {
        return (activeItemInHandLeft as MonoBehaviour).gameObject;
    }
}

