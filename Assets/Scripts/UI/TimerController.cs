using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerController : MonoBehaviour
{
    #region Singleton

    private static TimerController instance;

    private void Awake() {
        instance = this;
    }

    public static TimerController GetInstance() {
        return instance;
    }

    #endregion

    [SerializeField]
    private TMP_Text timerTextBox;

    private float currentTime = 0f;
    private bool isRunning;

    void Start() {
        isRunning = true;
    }

    void Update() {
        currentTime += Time.deltaTime;
        timerTextBox.text = string.Format("{0:.##}", currentTime);
    }

    public void StopTimer() {
        isRunning = false;
    }

    public float GetTime() {
        return currentTime;
    }
}

