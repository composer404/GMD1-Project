using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStat : MonoBehaviour
{
    [SerializeField]
    [Range(1, 100)]
    private int damage = 5;

    public int GetDamage() {
        return damage;
    }
}
