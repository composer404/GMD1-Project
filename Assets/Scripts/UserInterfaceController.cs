using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInterfaceController : MonoBehaviour
{
    
    [SerializeField]
    private GameObject messageInfoBox;

    [SerializeField]
    private GameObject messageText;

    public void OpenMessageInfoBox(string text) {
        messageInfoBox.SetActive(true);
        UnityEngine.UI.Text textComponent = messageText.GetComponent<UnityEngine.UI.Text>();

        textComponent.text = text;
    }

    public void CloseMessageInfoBox() {
        messageInfoBox.SetActive(false);
    }
}
