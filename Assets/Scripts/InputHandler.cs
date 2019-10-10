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

	//private float tolerance = 0.5f;
	//private float strikeTolerance = 0.5f;

	private bool wasStriking = false;

	public float lowNormalized { get; private set; }
	public float highNormalized { get; private set; }
	public float strikeNormalized { get; private set; }


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

		lowNormalized = scale(0, playerBands.tolerance, 0, 1, playerBands.low.maxPeak);
		highNormalized = scale(0, playerBands.tolerance, 0, 1, playerBands.high.maxPeak);
		strikeNormalized = scale(0, playerBands.strikeTolerance, 0, 1, playerBands.strikeBand.maxPeak);

		lowNormalized *= lowNormalized;
		highNormalized *= highNormalized;
		strikeNormalized *= strikeNormalized;

		if (playerBands.strikeBand.maxPeak > playerBands.strikeTolerance)
		{
			if (!wasStriking)
			{
				_power = 1;
				
			}else
			{
				_power = -1;
			}

			wasStriking = true;
			return;
		}
		else
		{
			wasStriking = false;
			_power = 0;
		}

		float delta = lowNormalized - highNormalized;
		if(delta > 0.1f)
		{
			_axis = 1;
		}
		if(delta < -0.1f)
		{
			_axis = -1;
		}

		/*if (playerBands.low.maxPeak > playerBands.tolerance && playerBands.low.maxPeak > playerBands.high.maxPeak)
		{
			_axis = 1;
		}
		if(playerBands.high.maxPeak > playerBands.tolerance && playerBands.high.maxPeak > playerBands.low.maxPeak)
		{
			_axis = -1;
		}*/
	}

	public float scale(float OldMin, float OldMax, float NewMin, float NewMax, float OldValue)
	{

		float OldRange = (OldMax - OldMin);
		float NewRange = (NewMax - NewMin);
		float NewValue = (((OldValue - OldMin) * NewRange) / OldRange) + NewMin;

		return (NewValue);
	}

}
