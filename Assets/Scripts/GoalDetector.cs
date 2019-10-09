using UnityEngine;
using System.Collections.Generic;

public class GoalDetector : MonoBehaviour
{
	public int playerIndex = 0;
	private void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Ball")
		{
			GameManager.instance.AddScore(playerIndex);
		}
	}
}

