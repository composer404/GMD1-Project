using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : MonoBehaviour
{
    [SerializeField]
    [Range(1, 100)]
    private int damage;

    [SerializeField]
    [Range(1, 100)]
    private int health;

    public int GetHealth() {
        return health;
    }

    public int GetDamage() {
        return damage;
    }

    public void GetDamage(int damage) {
        health -= damage;
    }
}
