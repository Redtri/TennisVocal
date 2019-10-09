using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour
{
    [SerializeField] private float bouncyness;
    [SerializeField] private Transform reference;
    public AK.Wwise.Event wwHitEvent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        Collider other = collision.collider;
        
        if (other.gameObject.name == "Ball")
        {
            //this.GetComponent<Rigidbody>().AddForce(collision.relativeVelocity.magnitude * other.transform.up * bouncyness);
            this.rebound(collision);
        }
    }

    
    public void rebound(Collision collision)
    {
        wwHitEvent.Post(gameObject);

        Collider ball = collision.collider;
        List<ContactPoint> contactPoints = new List<ContactPoint>();
        Vector3 pointOfImpact;
        print("Points colliding: " + collision.contacts.Length);
        print("First point that collided: " + collision.contacts[0].point);
        collision.GetContacts(contactPoints);
        pointOfImpact = collision.GetContact(collision.GetContacts(contactPoints)-1).point;
        
        Vector3 directionToRebound = (pointOfImpact - this.reference.transform.position).normalized;
        ball.gameObject.GetComponent<Rigidbody>().AddForce(collision.relativeVelocity.magnitude * directionToRebound.normalized * bouncyness);
    }
}
