using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    private Text timerTextBox;

    private float currentTime = 0f;
    private bool isRunning;

    void Start() {
        isRunning = true;
    }

    void Update() {
    }

    void FixedUpdate() {
        currentTime += Time.fixedDeltaTime;
        timerTextBox.text = string.Format("{0:0.00}", currentTime);
    }

    public void StopTimer() {
        isRunning = false;
    }

    public float GetTime() {
        return currentTime;
    }
}

