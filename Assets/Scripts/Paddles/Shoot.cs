using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour
{
	private Bounce bounce;

	private void Awake()
	{
		bounce = GetComponent<Bounce>();
	}


	private void OnTriggerStay(Collider other)
	{
		if(other.tag == "Ball")
		{
			bounce.SetActive(true);
		}
	}
}
