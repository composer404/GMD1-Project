using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : Collectable
{
    [SerializeField]
    private float throwSpeed = 8;

    public void Throw(Transform playerTransform) {
        print("Throw");

        transform.parent = null;
        Rigidbody rb = gameObject.AddComponent<Rigidbody>();

        rb.AddForce((playerTransform.forward + Vector3.up) * throwSpeed, ForceMode.Impulse);
    }
}
