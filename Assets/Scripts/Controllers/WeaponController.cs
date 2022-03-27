using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    private CapsuleCollider capsuleCollider;
    private WeaponStat weaponStat;
    private bool isAttacking;

    void Start() {
        capsuleCollider = gameObject.GetComponent<CapsuleCollider>();
        weaponStat = gameObject.GetComponent<WeaponStat>();
    }

    public IEnumerator Attack() {
        isAttacking = true;
        yield return new WaitForSeconds(0.3f);
        capsuleCollider.enabled = true;
        yield return new WaitForSeconds(0.3f);

        capsuleCollider.enabled = false;
        isAttacking = false;
    }

    public bool GetAttackState() {
        return isAttacking;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Enemy") {
            EnemyController controller = other.GetComponent<EnemyController>();
            if (controller == null) {
                return;
            }
    
            controller.GetDamage(weaponStat.getDamage());
        }
    }
}
