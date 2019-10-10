using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour
{
	private Rigidbody rb;
	private Vector3 v;
	private void Awake()
	{
		rb = GetComponent<Rigidbody>();
	}

	private void FixedUpdate()
	{
		v = rb.velocity;
	}
	private void OnCollisionEnter(Collision collision)
	{
		//Debug.Log(collision.gameObject);
		if(collision.gameObject.GetComponent<Bounce>() == null)
		{
			rb.velocity = Vector3.Reflect(v, collision.GetContact(0).normal) * 1.01f;
			//Vector3 dir = Vector3.Reflect(v.normalized, collision.GetContact(0).normal);
			//Debug.DrawRay(transform.position, dir.normalized * 10, Color.red, 100);
			//rb.AddForce(dir.normalized * 50,ForceMode.Impulse);
		}

		//Vector3 dir = 
		//rb.AddForce()

	}

}
