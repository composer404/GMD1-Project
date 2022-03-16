using UnityEngine;

public enum CollectableTypes {
    WEAPON,
    POTION,
    COIN,
}

public enum CollectPlaces {
    RIGHT_HAND,
    LEFT_HAND
}

public class Collectable : MonoBehaviour {
    [SerializeField]
    private string interactionText = "Press F to pickup";

    [SerializeField]
    private CollectableTypes collectableType;

    [SerializeField]
    private CollectPlaces collectPlace = CollectPlaces.RIGHT_HAND;

    [SerializeField]
    private Vector3 activePostion;
    
    [SerializeField]
    private Vector3 activeRotation;

    [SerializeField]
    private Vector3 activeScale;

    public string GetInteractionText() {
        return interactionText;
    }

    public CollectPlaces GetCollectPlace() {
        return collectPlace;
    }

    public CollectableTypes GetCollectableType() {
        return collectableType;
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

    public void SimulateMove(Vector3 moveVector) {        
         gameObject.GetComponent<Rigidbody>().AddForce(moveVector, ForceMode.Impulse);
    }
}