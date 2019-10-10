using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTrigger1 : MonoBehaviour {
    public AK.Wwise.Event wwSound;

    public void PlayE() {
        wwSound.Post(gameObject);
    }
}
