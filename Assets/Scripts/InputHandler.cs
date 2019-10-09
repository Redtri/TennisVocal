using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputHandler : MonoBehaviour, IInput {

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
        _clip = Microphone.Start(Microphone.devices[0], true, 5, 48000);
        _source.clip = _clip;
        _source.PlayDelayed(0.01f);
        Debug.Log(AudioSettings.outputSampleRate);		
	}

    void Update()
    {
		RefreshAudioSpectrum();
		UpdateInput();
    }

    private void RefreshAudioSpectrum() {
        AudioSpectrumHelper.GetAverageAmplitudes(_source, 4096, bands);
        AudioSpectrumHelper.SpectrumDisplay(_source, 4096);
        AudioSpectrumHelper.BandDisplay(_source, 4096, bands);
    }

	private void UpdateInput()
	{
		_axis = 0;
		
		if (bands[2].maxPeak > strikeTolerance)
		{
			if (!wasStriking)
			{
				_power = 1;
				return;
			}else
			{
				_power = 0;
			}

			wasStriking = true;
		}else
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
