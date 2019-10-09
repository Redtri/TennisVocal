
public class Note
{
	private float[] sums;

	public Note(int size)
	{
        sums = new float[size];
	}

	public void SetSums(Band[] bands, float ratio)
	{
		for(int  i= 0; i < bands.Length; i++)
		{
			sums[i] += (bands[i].sum-sums[i]) * ratio;
		}
	}

    public float[] GetSums() {
        return sums;
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

