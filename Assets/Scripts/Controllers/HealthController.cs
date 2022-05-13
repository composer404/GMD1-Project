using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    [SerializeField]
    private int health = 20;

    private PlayerStat playerStat;

    void Start() {
        playerStat = PlayerManager.GetInstance().GetPlayer().gameObject.GetComponent<PlayerStat>();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            PickUp();
        }
    }

    private void PickUp() {
        if(playerStat.GetHealth() >= 100) {
            return;
        }
        AudioManager.GetInstance().PlayHeart();
        playerStat.IncreaseHealth(health);
        PlayerController healthBarController = PlayerManager.GetInstance().GetPlayer().gameObject.GetComponent<PlayerController>();
        healthBarController.SetPlayerHealth(playerStat.GetHealth());
        Destroy(gameObject);
    }
}
