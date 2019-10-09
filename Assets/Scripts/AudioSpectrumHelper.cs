using UnityEngine;
using System.Collections;

public static class AudioSpectrumHelper
{
	public const float BAND_SMOOTHNESS = 1f;
	/// <summary>
	/// Update the sum of the bands
	/// </summary>
	/// <param name="source">Audiosource to listen to</param>
	/// <param name="spectrumLength">Length of spectrum (power of 2)</param>
	/// <param name="bands">The bands to update</param>
	public static void GetAverageAmplitudes(AudioSource source, int spectrumLength, Band[] bands)
	{
		float[] spectrum = new float[spectrumLength];
		float[] sums = new float[bands.Length];
		int[] count = new int[bands.Length];

		source.GetSpectrumData(spectrum, 0, FFTWindow.Rectangular);

		for(int i=0; i< bands.Length; i++)
		{
			bands[i].maxPeak = 0;
		}

		for(int i =0; i< spectrum.Length; i++)
		{
			for(int c =0; c < bands.Length; c++)
			{
				if(i > GetIndex(bands[c].min,spectrum.Length) && i < GetIndex(bands[c].max, spectrum.Length))
				{
					float value = (Mathf.Log(spectrum[i]) + 10) / 10;
					sums[c] += value;
					count[c]++;
					bands[c].maxPeak = bands[c].maxPeak > value ? bands[c].maxPeak : value;
				}
			}
		}

		for (int i = 0; i < bands.Length; i++)
		{
			float nSum = count[i] > 0 ? sums[i] / count[i] : 0;
			bands[i].sum = Mathf.Clamp01( Mathf.Lerp(bands[i].sum, nSum/* 1- (nSum-1)*(nSum - 1)*/, BAND_SMOOTHNESS *60 * Time.deltaTime));
		}
	}

	/// <summary>
	/// Convert hz to index (not precise)
	/// </summary>
	/// <param name="index"></param>
	/// <param name="spectrumLength"></param>
	/// <returns></returns>
	public static int GetIndex(int index, int spectrumLength)
	{
		return Mathf.FloorToInt(index * spectrumLength / AudioSettings.outputSampleRate * 2);
	}

	/// <summary>
	/// Display the spectrum
	/// </summary>
	/// <param name="source"></param>
	/// <param name="spectrumLength"></param>
	public static void SpectrumDisplay(AudioSource source, int spectrumLength)
	{
		float[] spectrum = new float[spectrumLength];
		source.GetSpectrumData(spectrum, 0, FFTWindow.Rectangular);

		for (int i = 0; i < spectrum.Length-1; i++)
		{
			Debug.DrawLine(new Vector3(i, Mathf.Log(spectrum[i]) + 10, 0), new Vector3(i+1, Mathf.Log(spectrum[i+1]) + 10, 0), Color.cyan);
		}	
	}

	/// <summary>
	/// Display bands
	/// </summary>
	public static void BandDisplay(AudioSource source, int spectrumLength, Band[] bands)
	{
		float[] spectrum = new float[spectrumLength];
		source.GetSpectrumData(spectrum, 0, FFTWindow.Rectangular);

		foreach (Band b in bands)
		{
			Debug.DrawRay(new Vector3(GetIndex(b.min, spectrum.Length), 0, 0), Vector3.up * 10, Color.green);
			Debug.DrawRay(new Vector3(GetIndex(b.max, spectrum.Length), 0, 0), Vector3.up * 10, Color.green);
			Debug.DrawLine(new Vector3(GetIndex(b.min, spectrum.Length), b.sum * 10, 0), new Vector3(GetIndex(b.max, spectrum.Length), b.sum * 10, 0), Color.green);
			Debug.DrawLine(new Vector3(GetIndex(b.min, spectrum.Length), b.maxPeak * 10, 0), new Vector3(GetIndex(b.max, spectrum.Length), b.maxPeak * 10, 0), Color.red);
		}
	}


}


