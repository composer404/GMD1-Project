using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    private Text pointText;

    [SerializeField]
    private Text killText;

    [SerializeField]
    private Text remainingCollectablesText;

    private int points = 0;
    private int kills = 0;

    private void Start() {
        RefreshCollectableText();
    }

    public int GetPoints() {
        return points;
    }

    public int GetKills() {
        return kills;
    }

    public void AddPoints(int points){ 
        this.points += points;
        this.pointText.text = this.points.ToString();
        RefreshCollectableText();
    }

    public void AddKill() {
        this.kills += 1;
         this.killText.text = this.kills.ToString();
    }

    public void RefreshCollectableText() {
        remainingCollectablesText.text = GeneralInfoManager.GetInstance().GetCollectablesToWin().ToString();
    }
}
