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
        gameObject.transform.position = new Vector3(transform.position.x,24,transform.position.z);
        gameObject.transform.Rotate(270,0,0);
    }

    void Update() {
        gameObject.transform.Rotate(0, 0, 50 * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            PickUp();
            CheckIfWin();
        }
    }

    private void PickUp() {
        AudioManager.GetInstance().PlayCoinCollect();
        generalInfoManager.Collect();
        pointManager.AddPoints(points);
        Destroy(gameObject);
    }

    private void CheckIfWin() {
       if (generalInfoManager.GetCollectablesToWin() <= 0) {
           gameStateManager.Win();
       }
    }
}
