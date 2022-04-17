using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWeaponController : Weapon
{
    [SerializeField]
    private GameObject prefab;

    [SerializeField]
    private float burstSpeed;

    [SerializeField]
    private Transform burstSpawnTransform;
    
    private bool isAttacking;
    private GameObject burst;

    void Start() {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override IEnumerator Attack() {

        isAttacking = true;
        yield return new WaitForSeconds(0.4f);

        SpawnBurst();
        ThrowBurst();

        yield return new WaitForSeconds(1f);

        if (burst != null) {
            Destroy(burst);
        }

        isAttacking = false;
     }

    public override bool GetAttackState()
    {
        return isAttacking; 
    }

    private void SpawnBurst() {

        burst = Instantiate(prefab, burstSpawnTransform.position, PlayerManager.GetInstance().GetPlayer().transform.rotation);
    }

    private void ThrowBurst() {
        AudioManager.GetInstance().PlayFire();
        Rigidbody burstRb = burst.GetComponent<Rigidbody>();
        burstRb.AddForce(burst.transform.forward * burstSpeed, ForceMode.Impulse);
    }
}
