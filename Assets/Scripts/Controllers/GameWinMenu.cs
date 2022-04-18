using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameWinMenu : MonoBehaviour
{
    [SerializeField]
    private Text killText;

    [SerializeField]
    private Text pointText;

    [SerializeField]
    private Text summaryText;

    [SerializeField]
    private Text newRecordText;

    [SerializeField]
    private Text timeText;

    [SerializeField]
    private Text timeMultiplerText;

    [SerializeField]
    private GameObject buttonSection;

    [SerializeField]
    private GameObject sumamrySection;

    private PointManager pointManager;
    private GeneralInfoManager generalInfoManager;
    private StorageManager storageManager;
    private TimerController timerController;
    
    void Start()
    {
        pointManager = PointManager.GetInstance();
        generalInfoManager = GeneralInfoManager.GetInstance();
        storageManager = StorageManager.GetInstance();
        timerController = TimerController.GetInstance();

        killText.text = pointManager.GetKills().ToString();
        pointText.text = pointManager.GetPoints().ToString();
        timeText.text = timerController.GetTime().ToString("0.00");

        float result = (pointManager.GetPoints() + (pointManager.GetKills() * generalInfoManager.GetKillPoints()) + (timerController.GetTime() * generalInfoManager.GetTimeMultiplyer()));
        float bestResult =  storageManager.GetBestResult();

        if(result > bestResult) {
            newRecordText.gameObject.SetActive(true);
        }

        summaryText.text = result.ToString("0.00");
        storageManager.SaveResult(result);
    }


    public void OpenButtonSection() {
        sumamrySection.SetActive(false);
        buttonSection.SetActive(true);
    }
}
