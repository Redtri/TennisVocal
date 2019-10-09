using UnityEngine;

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

