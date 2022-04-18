using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManger : MonoBehaviour
{
    #region Singleton

    private static CameraManger instance;

    private void Awake() {
        instance = this;
    }

    public static CameraManger GetInstance() {
        return instance;
    }

    #endregion

    [SerializeField]
    GameObject mainCamera;

    public GameObject GetMainCamera() {
        return mainCamera;
    }
}
