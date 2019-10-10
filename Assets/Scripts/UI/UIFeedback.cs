using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIFeedback : MonoBehaviour
{
	public InputHandler input;
	public Slider low;
	public Slider high;
	public Slider strike;

	private void Update()
	{
		low.value = input.lowNormalized;
		high.value = input.highNormalized;
		strike.value = input.strikeNormalized;
	}

}
