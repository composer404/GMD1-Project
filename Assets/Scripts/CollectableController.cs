using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CollectableController : Collectable
{

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
}