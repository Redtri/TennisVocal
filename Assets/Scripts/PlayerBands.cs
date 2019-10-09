using UnityEngine;
using System.Collections;
[CreateAssetMenu(fileName ="PlayerBands",menuName = "TennisVocal/playerBands",order = 0)]
public class PlayerBands : ScriptableObject
{
	public Band low;
	public Band high;
	public Band strikeBand;

	public KeyCode lowKey = KeyCode.W;
	public KeyCode highKey = KeyCode.X;

	public void UpdateBands(AudioSource src,int length)
	{
		if (Input.GetKey(lowKey))
		{
			low = Capture(low, src, 500, 40, length);
		}

		if (Input.GetKey(highKey))
		{
			high = Capture(high, src, 500, 40, length);
		}

		strikeBand.min = low.min;
		strikeBand.max = high.max;
	}

	private Band Capture(Band b, AudioSource src, int maxHz, int bandSize, int length)
	{
		Band[] bs = new Band[maxHz / bandSize];
		for (int i = 0; i < bs.Length; i++)
		{
			bs[i].min = i * bandSize;
			bs[i].max = (i + 1) * bandSize;
		}

		AudioSpectrumHelper.GetAverageAmplitudes(src, length, bs);
		AudioSpectrumHelper.SpectrumDisplay(src, length);
		AudioSpectrumHelper.BandDisplay(src, length, bs);
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
		return b;
	}

	public Band[] GetBands()
	{
		return new Band[] { low, high, strikeBand };
	}

	public void UpdateValue(Band[] bands)
	{
		low = bands[0];
		high = bands[1];
		strikeBand = bands[2];
	}
}
