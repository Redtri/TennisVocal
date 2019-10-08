using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Micro : MonoBehaviour
{
    public Microphone micro;
    public AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        source = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            source.clip = Microphone.Start("", false, 5, 44100);
        }
        if (Input.GetKeyDown(KeyCode.E)) {
            if (!source.isPlaying && source.clip != null) {
                source.Play();
            }
        }
    }
}
