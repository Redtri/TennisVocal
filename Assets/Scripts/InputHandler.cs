using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum eINPUT { LEFT, RIGHT, STRIKE };

public class InputHandler : MonoBehaviour {

    [SerializeField]
    private Paddle paddle;
    private Band[] _bands;
    

    //REFERENCES
    public Text captureState;
    private AudioSource _source;
    private AudioClip _clip;

    //CAPTURE
    public float captureDuration;
    private float captureTime;
    private bool isCapturing;

    //INPUT DATA
    private eINPUT currentInput;
    private List<Note> inputs;


    // Start is called before the first frame update
    void Start()
    {
        _source = GetComponent<AudioSource>();
        foreach (string str in Microphone.devices) {
            Debug.Log(str);
        }
        _clip = Microphone.Start(Microphone.devices[0], true, 5, 48000);
        _source.clip = _clip;
        _source.PlayDelayed(0.01f);
        Debug.Log(AudioSettings.outputSampleRate);
        
        inputs = new List<Note>();

        captureTime = captureDuration;
        isCapturing = false;

        currentInput = eINPUT.LEFT;
        SetBands(700, 20);
    }

    // Update is called once per frame
    void Update()
    {
        RefreshAudioSpectrum();
        HandleCapture();
        //captureState.text = isCapturing + " " + currentInput;
    }

    private void RefreshAudioSpectrum() {
        AudioSpectrumHelper.GetAverageAmplitudes(_source, 4096, _bands);
        AudioSpectrumHelper.SpectrumDisplay(_source, 4096);
        AudioSpectrumHelper.BandDisplay(_source, 4096, _bands);
    }

    private void HandleCapture() {
    //ATM : Handles notes capture for the input setting
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (!isCapturing) {
                captureState.text = "Capture";
                isCapturing = true;
                inputs.Add(new Note(700/20));
            }
        }

        if (isCapturing){
            //Timer over
            if (HandleTimer()) {
                captureState.text = "Stop";
                isCapturing = false;
                ++currentInput;
            } else {
                inputs[(int)currentInput].SetSums(_bands, Time.deltaTime);
            }
        }

        if (Input.GetKeyDown(KeyCode.E)) {
            for(int i = 0; i < inputs.Count; i++) {
                Debug.Log(i);
                foreach(float f in inputs[i].GetSums()) {
                    Debug.Log(f);
                }
            }
        }
    }

    //Returns whether or not the time is reached
    private bool HandleTimer() {
        if (captureTime > 0f) {
            captureTime -= Time.deltaTime;

            return false;
        } else {
            captureTime = captureDuration;

            return true;
        }
    }

    public void SetBands(int maxHz, int size) {
        _bands = new Band[maxHz / size];
        for (int i = 0; i < _bands.Length; i++) {
            _bands[i].min = i * size;
            _bands[i].max = (i + 1) * size;
        }
    }
}
