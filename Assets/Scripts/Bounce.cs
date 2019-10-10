using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour
{

    [SerializeField] public float bounciness;
    [SerializeField] private Transform reference;
    public AK.Wwise.Event wwHitEvent;

	private BoxCollider col;

    // Start is called before the first frame update
    void Awake()
    {
		col = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void SetActive(bool b)
	{
		col.enabled = b;
	}

    void OnCollisionEnter(Collision collision)
    {
        Collider other = collision.collider;
		//Debug.Log("collision" + collision.gameObject);
        if (other.gameObject.name == "Ball")
        {
            //this.GetComponent<Rigidbody>().AddForce(collision.relativeVelocity.magnitude * other.transform.up * bouncyness);
            this.rebound(collision);
			SetActive(false);
        }
    }

    
    void rebound(Collision collision) {
		//Debug.Log("rebound");
        wwHitEvent.Post(gameObject);
        Collider ball = collision.collider;
        List<ContactPoint> contactPoints = new List<ContactPoint>();
        Vector3 pointOfImpact;
        print("Points colliding: " + collision.contacts.Length);
        print("First point that collided: " + collision.contacts[0].point);
        collision.GetContacts(contactPoints);
        pointOfImpact = collision.GetContact(collision.GetContacts(contactPoints)-1).point;
        Vector3 directionToRebound = (pointOfImpact - this.reference.transform.position).normalized;
        //Debug.DrawRay(pointOfImpact, directionToRebound);
        //force = directionToRebound.magnitude;
        //directionToRebound = Vector3.ProjectOnPlane(directionToRebound, Vector3.up);
        //directionToRebound += Vector3.up;
        //directionToRebound = directionToRebound.normalized;
        //directionToRebound += Vector3.up * directionToRebound.magnitude/2;
        //directionToRebound = directionToRebound.normalized;
        //directionToRebound *= force;
        //Debug.DrawRay(pointOfImpact, directionToRebound.normalized * 10);
        //directionToRebound = Vector3.Reflect(-directionToRebound, Vector3.up);
        //Debug.DrawRay(pointOfImpact, directionToRebound.normalized * 10);
        ball.gameObject.GetComponent<Rigidbody>().AddForce(collision.relativeVelocity.magnitude * directionToRebound * bounciness);
        //ball.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
    }

}
