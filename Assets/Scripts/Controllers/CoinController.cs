using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    [SerializeField]
    private int points = 10;
    
    private PointManager pointManager;
    private GeneralInfoManager generalInfoManager;
    private GameStateManager gameStateManager;

    void Start() {
        pointManager = PointManager.GetInstance();
        generalInfoManager = GeneralInfoManager.GetInstance();
        gameStateManager = GameStateManager.GetInstance();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            PickUp();
            CheckIfWin();
        }
    }

    private void PickUp() {
        pointManager.AddPoints(points);
        Destroy(gameObject);
    }

    private void CheckIfWin() {
       if (pointManager.GetPoints() >= generalInfoManager.GetPointsToWin()) {
           gameStateManager.Win();
       }
    }
}
