using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    [SerializeField]
    [Range(1, 100)]
    private int damage;

    [SerializeField]
    private int health;

    [SerializeField]
    private int maxHealth;

    public int GetHealth() {
        return health;
    }

    public int GetDamage() {
        return damage;
    }

    public int GetMaxHealth() {
        return maxHealth;
    }

    public void GetDamage(int damage) {
        health -= damage;
    }

    public void IncreaseDamage(int damage) {
        this.damage += damage;
    }

    public void DecreaseDamage(int damage) {
        this.damage -= damage;
    }
}
