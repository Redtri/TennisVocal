using UnityEngine;

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
		int count = 0;
		for(int i = 0; i< bands.Length; i++)
		{
			float delta = Mathf.Abs(sums[i] - bands[i].sum);
			/*if(sums[i] > 0.25f && bands[i].sum > 0.25f)
			{
				e += 1;
			} */
			if(delta > 0.1f)
			{
				e++;
			}else
			{

			}
			count++;
			//e += delta;
			
		}
		e /= count;
		return 1 -e;
	}
}

