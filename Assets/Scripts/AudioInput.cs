using UnityEngine;
using System.Collections;

public class AudioInput : MonoBehaviour
{

	[SerializeField]
	private Paddle paddle;
	[SerializeField]
	private Band[] bands;


	private AudioSource _source;

	private AudioClip _clip;

	private void Awake()
	{
		_source = GetComponent<AudioSource>();
		_clip = Microphone.Start(Microphone.devices[0], true, 5, 48000);
		_source.clip = _clip;
		_source.PlayDelayed(0.01f);
		Debug.Log(AudioSettings.outputSampleRate);
	}

	private void Update()
	{
		AudioSpectrumHelper.GetAverageAmplitudes(_source, 2048, bands);
		AudioSpectrumHelper.SpectrumDisplay(_source, 2048);
		AudioSpectrumHelper.BandDisplay(_source, 2048, bands);

		if (IsUUUUH())
		{
			Debug.Log("UUUUH");
			paddle.Move(-1);
		}else
		{
			if (isIIIIH())
			{
				Debug.Log("IIIIH");
				paddle.Move(1);
			}
			else
			{
				if (ISAAAAH()){
					Debug.Log("AAAAAh");
				}
			}
		}
	}

	private bool ISAAAAH()
	{
		if (bands[3].sum > 0.05f && bands[3].sum > bands[2].sum && bands[3].sum > bands[1].sum)
		{
			return true;
		}
		return false;
	}

	private bool IsUUUUH()
	{
		if(bands[1].sum > 0.05f && bands[1].sum > bands[2].sum && bands[1].sum > bands[3].sum)
		{
			return true;
		}
		return false;
	}

	private bool isIIIIH()
	{
		if (bands[2].sum > 0.05f && bands[2].sum > bands[1].sum && bands[2].sum > bands[3].sum)
		{
			return true;
		}
		return false;
	}

}
