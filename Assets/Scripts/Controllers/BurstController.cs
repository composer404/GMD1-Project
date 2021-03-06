using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurstController : MonoBehaviour
{
    // Start is called before the first frame update
    private WeaponStat weaponStat;

    void Start()
    {
        weaponStat = gameObject.GetComponent<WeaponStat>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Enemy") {
            EnemyController controller = null;

            if (other.transform.parent == null) {
                controller = other.transform.GetComponent<EnemyController>();
            }

            if(other.transform.parent != null) {
                controller = other.transform.parent.GetComponent<EnemyController>();
            }
            
            if (controller == null) {
                return;
            }
    
            controller.GetDamage(weaponStat.GetDamage());
            Destroy(gameObject);
        }
    }
}
