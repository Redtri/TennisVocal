using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTrigger : MonoBehaviour {
    public AK.Wwise.Event wwSound;

    public void Play() {
        wwSound.Post(gameObject);
    }
}
