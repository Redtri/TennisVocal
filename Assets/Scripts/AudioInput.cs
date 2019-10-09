using UnityEngine;
using System.Collections;

public class AudioInput : MonoBehaviour
{

	[SerializeField]
	private Paddle paddle;
	[SerializeField]
	private Band[] _bands;
	public Band[] bands { get { return _bands; } }

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

		SetBands(700,20);
		test = new Note(_bands);
		
	}

	private void Update()
	{
		AudioSpectrumHelper.GetAverageAmplitudes(_source, 4096, _bands);
		AudioSpectrumHelper.SpectrumDisplay(_source, 4096);
		AudioSpectrumHelper.BandDisplay(_source, 4096, _bands);

		if (Input.GetMouseButton(0))
		{
			test.SetSums(_bands);
		}
		else
		{
			float eval = test.Evaluate(_bands);
			if(eval > 0.7)
			{
				Debug.Log("AAAAAH");
			}
		
		}
	}

	public void SetBands(int maxHz, int size)
	{
		_bands = new Band[maxHz / size];
		for(int i =0; i< _bands.Length; i++)
		{
			_bands[i].min = i * size;
			_bands[i].max = (i + 1) * size;
		}
	}
}
