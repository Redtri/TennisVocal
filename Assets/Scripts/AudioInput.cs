using UnityEngine;
using System.Collections;

public class AudioInput : MonoBehaviour
{

	[SerializeField]
	private Paddle paddle;
	[SerializeField]
	private Band[] bands;

	Note test;


	private AudioSource _source;

	private AudioClip _clip;

	private void Awake()
	{
		_source = GetComponent<AudioSource>();
		foreach(string str in Microphone.devices)
		{
			Debug.Log(str);
		}
		_clip = Microphone.Start(Microphone.devices[0], true, 5, 48000);
		_source.clip = _clip;
		_source.PlayDelayed(0.01f);
		Debug.Log(AudioSettings.outputSampleRate);

		test = new Note(bands);
	}

	private void Update()
	{
		AudioSpectrumHelper.GetAverageAmplitudes(_source, 4096, bands);
		AudioSpectrumHelper.SpectrumDisplay(_source, 4096);
		AudioSpectrumHelper.BandDisplay(_source, 4096, bands);

	
	}
}
