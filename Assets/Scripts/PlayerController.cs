using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using ScriptManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private GameObject Hand;

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

    private Rigidbody rb;
    private Animator animator;
    private Vector3 movementDirection;
    
    private bool isShortJump;
    private bool isPickup;

    private CollectableController collectableInArea;
    private CollectableController activeItemInHand;

    void Start() {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }



    void Update() {
        // if (isShortJump) {
        //     rb.AddForce(Vector3.up * (isShortJump ? shortJumpSpeed : longJumpSpeed));
        //     isShortJump = false;
        // }

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
        // isShortJump = true;
        // animator.SetTrigger("ShortJump");
    }

    void OnShortAttack() {
        // animator.SetTrigger("ShortAttack");
    }

    void OnPickup() {
        if (collectableInArea != null) {
            animator.SetTrigger("PickUp");
            Invoke("PickupItemAfterAnimation", 1f);
        }
    }

    void OnDrop() {
        GameObject activeItem = (activeItemInHand as MonoBehaviour).gameObject;

        if (activeItem) {
            Rigidbody itemRb = activeItem.AddComponent<Rigidbody>();
            itemRb.AddForce(transform.forward * 2.0f, ForceMode.Impulse);
            
            Invoke("PlaceItemOnGround", 0.25f);
        }
    }


    /* -------------------------------------------------------------------------- */
    /*                               INVOCES METHODS                              */
    /* -------------------------------------------------------------------------- */

    private void PlaceItemOnGround() {
        Destroy((activeItemInHand as MonoBehaviour).GetComponent<Rigidbody>());
        activeItemInHand.transform.parent = null;
        activeItemInHand = null;
    }

    private void PickupItemAfterAnimation() {
        collectableInArea.OnPickup();
        SetItemActive();
        collectableInArea.OnActive();
    }


    /* -------------------------------------------------------------------------- */
    /*                                  TRIGGERS                                  */
    /* -------------------------------------------------------------------------- */

    private void OnTriggerEnter(Collider other) {
        CollectableController collectable = other.GetComponent<CollectableController>();

        if(collectable != null) {
            collectableInArea = collectable;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (collectableInArea != null) {
            collectableInArea = null;
        }
    }

    /* -------------------------------------------------------------------------- */
    /*                               HELP FUNCTIONS                               */
    /* -------------------------------------------------------------------------- */


    private void SetItemActive()
    {
        GameObject activeItem = (collectableInArea as MonoBehaviour).gameObject;
        activeItem.SetActive(true);
        activeItem.transform.parent = Hand.transform;
        activeItemInHand = collectableInArea;
    }
}

