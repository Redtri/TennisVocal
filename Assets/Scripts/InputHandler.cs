using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputHandler : MonoBehaviour, IInput {

	public int micIndex = 0;
	public bool keyBoardControl = false;

	public PlayerBands playerBands;

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
	}

    void Update()
    {
		playerBands.UpdateBands(_source, 4096);
		bands = playerBands.GetBands();
		RefreshAudioSpectrum();
		
		playerBands.UpdateValue(bands);
		
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


	private void UpdateMicInput()
	{
		_axis = 0;

		if (playerBands.strikeBand.maxPeak > strikeTolerance)
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

		if (playerBands.low.maxPeak > tolerance && playerBands.low.maxPeak > playerBands.high.maxPeak)
		{
			_axis = 1;
		}
		if(playerBands.high.maxPeak > tolerance && playerBands.high.maxPeak > playerBands.low.maxPeak)
		{
			_axis = -1;
		}
	}
}
