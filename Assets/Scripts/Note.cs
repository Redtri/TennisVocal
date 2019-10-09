
public class Note
{
	private float[] sums;

	public Note(Band[] bands)
	{
		sums = new float[bands.Length];
	}


	public void SetSums(Band[] bands)
	{
		for(int  i= 0; i < bands.Length; i++)
		{
			sums[i] = bands[i].sum;
		}
	}


	public float Evaluate(Band[] bands)
	{
		float e = 0;
		for(int i = 0; i< bands.Length; i++)
		{
			e += (sums[i] - bands[i].sum);
		}
		e /= sums.Length;
		return e;
	}

}

