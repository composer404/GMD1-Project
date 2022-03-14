using UnityEngine;

enum CollectableTypes  {
    WEAPON,
    POTION,
}

public class Collectable : MonoBehaviour {
    [SerializeField]
    private string interactionText = "Press F to pickup";

    [SerializeField]
    private CollectableTypes collectableType;

    public string GetInteractionText() {
        return interactionText;
    }
}