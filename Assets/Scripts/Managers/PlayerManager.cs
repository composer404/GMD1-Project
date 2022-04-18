using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    #region Singleton

    private static PlayerManager instance;

    private void Awake() {
        instance = this;
    }

    public static PlayerManager GetInstance() {
        return instance;
    }

    #endregion

    [SerializeField]
    private GameObject player;

    public GameObject GetPlayer() {
        return player;
    }
}
