using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    [SerializeField]
    private int points = 10;
    
    private PointManager pointManager;

    void Start() {
        pointManager = PointManager.GetInstance();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            PickUp();
        }
    }

    private void PickUp() {
        pointManager.AddPoints(points);
        Destroy(gameObject);
    }
}
