using UnityEngine;
using System.Collections.Generic;

public class GoalDetector : MonoBehaviour
{
	public AK.Wwise.Event wwGoalEvent;

	public int playerIndex = 0;
	private void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Ball")
		{
			wwGoalEvent.Post(gameObject);
			GameManager.instance.AddScore(playerIndex);
		}
	}
}

