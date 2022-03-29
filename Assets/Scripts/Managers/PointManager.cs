using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointManager : MonoBehaviour
{
    #region Singleton

    private static PointManager instance;

    private void Awake() {
        instance = this;
    }

    public static PointManager GetInstance() {
        return instance;
    }

    #endregion

    [SerializeField]
    private TMP_Text pointText;

    private int points;

    public int GetPoints() {
        return points;
    }

    public void AddPoints(int points){ 
        this.points += points;
        this.pointText.text = $"POINTS: {this.points}";
    }
}
