using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{

    #region Singleton

    private static GameStateManager instance;

    private void Awake() {
        instance = this;
    }

    public static GameStateManager GetInstance() {
        return instance;
    }

    #endregion

    [SerializeField]
    private GameObject gameOverScreen;

    public void GameOver() {
        PauseGame();
        StorageManager.GetInstance().SaveResult();
        gameOverScreen.SetActive(true);
    }
    
    private void PauseGame() {
        Time.timeScale = 0;
    }

    private void ResumeGame() {
        Time.timeScale = 1;
    }
}
