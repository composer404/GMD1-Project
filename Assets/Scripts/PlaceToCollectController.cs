using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceToCollectController : MonoBehaviour
{
    [SerializeField]
    private GameObject pointsUIBox;

    private PointsManager pointsManager;
    private UnityEngine.UI.Text pointsText;
    
    void Start()
    {
        pointsManager = PointsManager.GetPointsManager();
        pointsText = pointsUIBox.GetComponent<UnityEngine.UI.Text>();
        pointsText.text = $"Points: {pointsManager.GetPoints()}";
    }

    void OnTriggerEnter(Collider other){
        if (other.tag == GameObjectTags.Coin) {
            pointsManager.AddPoints(((int)POINTS.STANDARD));
            pointsText.text = $"Points: {pointsManager.GetPoints()}";
        }
    }
}
