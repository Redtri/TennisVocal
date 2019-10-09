using UnityEngine;
using System.Collections.Generic;

public class GoalDetector : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Ball")
		{
			//call gamemanager
		}
	}
}

