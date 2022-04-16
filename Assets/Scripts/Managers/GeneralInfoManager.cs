using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralInfoManager : MonoBehaviour
{
    #region Singleton

    private static GeneralInfoManager instance;

    private void Awake() {
        instance = this;
    }

    public static GeneralInfoManager GetInstance() {
        return instance;
    }

    #endregion

    [SerializeField]
    private int pointsToWin = 30;

    [SerializeField]
    private int enemiesNumber = 5;

    [SerializeField]
    private int killPoints = 20;

    [SerializeField]
    private float timeMultiplier = 0.5f;


    public int GetEnemiesNumber() {
        return enemiesNumber;
    }

    public int GetPointsToWin() {
        return pointsToWin;
    }

    public int GetKillPoints() {
        return killPoints;
    }

    public float GetTimeMultiplyer() {
        return timeMultiplier;
    }

}
