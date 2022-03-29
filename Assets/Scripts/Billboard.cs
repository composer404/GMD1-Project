using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    private GameObject mainCamera;

    void Start() {
        mainCamera = CameraManger.GetInstance().GetMainCamera();
    }

    void LateUpdate() {
        transform.LookAt(transform.position + mainCamera.transform.forward);
    }
}
