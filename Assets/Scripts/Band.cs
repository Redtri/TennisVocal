using UnityEngine;

[System.Serializable]
public struct Band
{
	public int min, max;
	[HideInInspector]
	public float sum;
}
