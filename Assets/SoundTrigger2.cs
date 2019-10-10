using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTrigger2 : MonoBehaviour {
    public AK.Wwise.Event wwSound;

    public void PlayR() {
        wwSound.Post(gameObject);
    }
}
