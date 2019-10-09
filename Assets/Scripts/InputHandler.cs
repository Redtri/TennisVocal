using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputHandler : MonoBehaviour, IInput {

	public int micIndex = 0;
	public bool keyBoardControl = false;
	[SerializeField]
	private Band[] bands = new Band[3];
   
    private AudioSource _source;
    private AudioClip _clip;

	private int _axis = 0;
	public int axis => _axis;
	private float _power = 0;
	public float power => _power;

	[SerializeField]
	private float tolerance = 0.5f;
	[SerializeField]
	private float strikeTolerance = 0.5f;

	private bool wasStriking = false;


    void Awake()
    {
        _source = GetComponent<AudioSource>();
        foreach (string str in Microphone.devices) {
            Debug.Log(str);
        }
        _clip = Microphone.Start(Microphone.devices[micIndex], true, 5, 48000);
        _source.clip = _clip;
        _source.PlayDelayed(0.01f);
        Debug.Log(AudioSettings.outputSampleRate);
		bands[2].min = 0;
		bands[2].max = 0;
	}

    void Update()
    {
		RefreshAudioSpectrum();
		if (Input.GetKey(KeyCode.W))
		{
			Capture(0, 500, 40);
		}
		if (Input.GetKey(KeyCode.X))
		{
			Capture(1, 500, 40);
		}

		if (keyBoardControl)
		{
			UpdateKeyboardInput();
		}else
		{
			UpdateMicInput();
		}
	}

    private void RefreshAudioSpectrum() {
        AudioSpectrumHelper.GetAverageAmplitudes(_source, 4096, bands);
        AudioSpectrumHelper.SpectrumDisplay(_source, 4096);
        AudioSpectrumHelper.BandDisplay(_source, 4096, bands);
    }

	private void UpdateKeyboardInput()
	{
		if(micIndex == 0)
		{
			_axis = (int)Input.GetAxis("Horizontal");
			_power = Input.GetKeyDown(KeyCode.Space) ? 1 : 0;
		}
		else
		{
			_axis = (int)Input.GetAxis("Vertical");
			_power = Input.GetKeyDown(KeyCode.A) ? 1 : 0;
		}
	}

	private void Capture(int index, int maxHz,  int bandSize)
	{
		Band[] bs= new Band[maxHz/bandSize];
		for(int i = 0; i < bs.Length; i++)
		{
			bs[i].min = i * bandSize;
			bs[i].max = (i + 1) * bandSize;
		}

		AudioSpectrumHelper.GetAverageAmplitudes(_source, 4096, bs);
		AudioSpectrumHelper.BandDisplay(_source, 4096, bs);
		int highest = 0;
		for(int i = 0; i< bs.Length; i++)
		{
			if(bs[i].maxPeak > 0.5f)
			{
				highest = i;
				break;
			}
		}

		bands[index].min = bs[highest].min;
		bands[index].max = bs[highest].max;
		bands[2].min = bands[0].min;
		bands[2].max = bands[1].max;
	}

	private void UpdateMicInput()
	{
		_axis = 0;
		
		if (bands[2].maxPeak > strikeTolerance)
		{
			if (!wasStriking)
			{
				_power = 1;
				
			}else
			{
				_power = 0;
			}

			wasStriking = true;
			return;
		}
		else
		{
			wasStriking = false;
			_power = 0;
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
}
