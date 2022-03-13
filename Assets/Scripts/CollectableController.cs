using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CollectableController : MonoBehaviour
{

    [SerializeField]
    private Vector3 activePostion;
    
    [SerializeField]
    private Vector3 activeRotation;

    [SerializeField]
    private Vector3 activeScale;

    [SerializeField]
    private Vector3 dropRotation;

    public void OnDrop() {
        RaycastHit hit = new RaycastHit();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 1000))
        {
            gameObject.SetActive(true);
            gameObject.transform.position = hit.point;
            gameObject.transform.eulerAngles = dropRotation;
        }
    }

    public void OnPickup() {
        Destroy(gameObject.GetComponent<Rigidbody>());
        gameObject.SetActive(false);
    }

    public void OnActive() {
        transform.localPosition = activePostion;
        transform.localEulerAngles = activeRotation;
        transform.localScale = activeScale;
    }


}