using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    #region Singleton

    private static HealthManager instance;

    private void Awake() {
        instance = this;
    }

    public static HealthManager GetInstance() {
        return instance;
    }

    #endregion

    private int health = 0;
    private int kills = 0;

    public int GetHealth() {
        return health;
    }

    public int GetKills() {
        return kills;
    }

    public void AddHealth(int health){ 
        this.health += health;
    }

    public void AddKill() {
        this.kills += 1;
    }
}
