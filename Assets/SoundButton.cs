using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundButton : MonoBehaviour {

    public AK.Wwise.Event wwClickEvent;
    public AK.Wwise.Event wwHoverEvent;

    private void OnEnable() {
        GetComponent<Button>().onClick.AddListener(PressSound);
    }

    private void OnDisable() {
        GetComponent<Button>().onClick.RemoveListener(PressSound);
    }

    private void PressSound() {
        wwClickEvent.Post(gameObject);
    }

    public void HoverSound() {
        wwHoverEvent.Post(gameObject);
    }
}
