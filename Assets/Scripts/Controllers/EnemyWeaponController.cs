using UnityEngine;

public class EnemyWeaponController : MonoBehaviour
{
    private Transform player;
    private WeaponStat weaponStat;

    void Start() {
        player = PlayerManager.GetInstance().GetPlayer().transform;
        weaponStat = GetComponent<WeaponStat>();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            PlayerController controller = other.GetComponent<PlayerController>();
            if (controller != null) {
                StartCoroutine(controller.GetHit(weaponStat.GetDamage(), 0f));
            }
        }
    }
}
