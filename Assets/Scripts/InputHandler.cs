using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum eINPUT { LEFT, RIGHT, STRIKE };

public class InputHandler : MonoBehaviour {

	[SerializeField]
	private Paddle paddle;
	[SerializeField]
	private Band[] bands = new Band[3];
    

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
	private int _axis = 0;
	public int axis => _axis;
	[SerializeField]
	private float tolerance = 0.5f;
	[SerializeField]
	private float strikeTolerance = 0.5f;



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
		
	}

    // Update is called once per frame
    void Update()
    {

		RefreshAudioSpectrum();
		UpdateInput();
		paddle.Move(_axis);
       // HandleCapture();
    }

    private void RefreshAudioSpectrum() {
        AudioSpectrumHelper.GetAverageAmplitudes(_source, 4096, bands);
        AudioSpectrumHelper.SpectrumDisplay(_source, 4096);
        AudioSpectrumHelper.BandDisplay(_source, 4096, bands);
    }

	private void UpdateInput()
	{
		/*float maxPeak = 0;
		float maxSum = 0;
		foreach(Band b in bands)
		{
			maxPeak = maxPeak > b.maxPeak ? maxPeak : b.maxPeak;
			maxSum = maxSum > b.sum ? maxSum : b.sum;
		}*/
		_axis = 0;
		
		if(bands[2].maxPeak > strikeTolerance)
		{
			Debug.Log("Strike");
			return;
		}

		if (bands[0].maxPeak > tolerance && bands[0].maxPeak > bands[1].maxPeak)
		{
			_axis = 1;
		}
		if(bands[1].maxPeak > tolerance && bands[1].maxPeak > bands[0].maxPeak)
		{
			_axis = -1;
		}
	}

    private void HandleCapture() {
    //ATM : Handles notes capture for the input setting
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (!isCapturing) {
                captureState.text = "Capture " + currentInput;
                isCapturing = true;
                inputs.Add(new Note(700/20));
            }
        }

        if (isCapturing){
            //Timer over
            if (HandleTimer()) {
                captureState.text = "Stop " + currentInput;
                isCapturing = false;
                ++currentInput;
            } else {
                inputs[(int)currentInput].SetSums(bands, Time.deltaTime);
            }
        }

        if(inputs.Count > 2) {
            int detected = -1;
            float sureRatio = 0f;

            for (int i = 0; i < inputs.Count; i++) {
                if ((sureRatio = inputs[i].Evaluate(bands)) >= .7f) {
                    detected = i;
                    break;
                }
            }

            switch (detected) {
                case 0:
                    print((eINPUT)detected);
                    paddle.Move(-1);
                    break;
                case 1:
                    print((eINPUT)detected);
                    paddle.Move(1);
                    break;
                case 2:
                    print((eINPUT)detected);
                    break;
                default:
                    break;
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
		bands = new Band[maxHz / size];
		for (int i = 0; i < maxHz / size; i++) {
			bands[i].min = i * size;
			bands[i].max = (i + 1) * size;
        }
    }
}
