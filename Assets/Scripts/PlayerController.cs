using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed = 10f;

    [SerializeField]
    private GameObject ball;

    [SerializeField]
    private Transform hands;

    [SerializeField]
    private Transform postionOverHead;

    private Rigidbody rb;
    private Vector3 movementDirection;
    private bool isItemHold;

    void Awake() {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate() {
        if (isItemHold) {
            MoveItem();
        }

        if(movementDirection == Vector3.zero) {
            return;
        }

        Quaternion targetRotation = Quaternion.LookRotation(movementDirection);

        rb.MovePosition(rb.position + movementDirection * speed * Time.fixedDeltaTime);
        rb.MoveRotation(targetRotation);

    }

    private void MoveItem() {
        var ballRb = ball.GetComponent<Rigidbody>();
        ballRb.position = postionOverHead.transform.position;
    }
   

    /* -------------------------------------------------------------------------- */
    /*                                    INPUTS                                   */
    /* -------------------------------------------------------------------------- */

    void OnMove(InputValue movement) {
        Vector2 movementVector = movement.Get<Vector2>(); 
        movementDirection = new Vector3(movementVector.x, 0.0f, movementVector.y); 
    } 

    void OnPickUp() {
        print("pickUP");
        isItemHold = true;
        hands.localEulerAngles = Vector3.right * 180;
    }
}
