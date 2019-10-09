using UnityEngine;
using System.Collections;
[CreateAssetMenu(fileName ="PlayerBands",menuName = "TennisVocal/playerBands",order = 0)]
public class PlayerBands : ScriptableObject
{
	public Band low;
	public Band high;

	public KeyCode lowKey = KeyCode.W;
	public KeyCode highKey = KeyCode.X;

	public void Update(AudioSource src,int length)
	{
		if (Input.GetKey(lowKey))
		{

		}

		if (Input.GetKey(highKey))
		{

		}
	}

	private void Capture(Band b, AudioSource src, int maxHz, int bandSize)
	{
		Band[] bs = new Band[maxHz / bandSize];
		for (int i = 0; i < bs.Length; i++)
		{
			bs[i].min = i * bandSize;
			bs[i].max = (i + 1) * bandSize;
		}

		AudioSpectrumHelper.GetAverageAmplitudes(src, 4096, bs);
		AudioSpectrumHelper.BandDisplay(src, 4096, bs);
		int highest = 0;
		for (int i = 0; i < bs.Length; i++)
		{
			if (bs[i].maxPeak > 0.5f)
			{
				highest = i;
				break;
			}
		}

		b.min = bs[highest].min;
		b.max = bs[highest].max;
	}
}
