using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Service : MonoBehaviour
{

   // [SerializeField] private Transform Player1;
   // [SerializeField] private Transform Player2;

	public Transform[] players = new Transform[2];

	private Rigidbody rb;


    void OnEnable()
    {
        GameManager.instance.onService += serviceStart;
    }

    void OnDisable()
    {
        GameManager.instance.onService -= serviceStart;
    }

    // Start is called before the first frame update
    void Awake()
    {
		rb = GetComponent<Rigidbody>();
       // this.serviceLaunch(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


     void serviceStart(int playerIndex)
    {
		/*switch (playerIndex)
        {
            case 0:
                //this.transform.SetParent(Player1);
                
                break;
            case 1:
                //this.transform.SetParent(Player2);
                break;
            default:
                break;
        }*/
		SetService(playerIndex);
        //this.transform.localPosition = new Vector3(0, 0, 12);
		StartCoroutine(delayLaunch(playerIndex));
    }

	private IEnumerator delayLaunch(int id)
	{
		yield return new WaitForSecondsRealtime(1);
		serviceLaunch(id);
	}

	private void SetService(int id)
	{
		rb.velocity = Vector3.zero;
		rb.angularVelocity = Vector3.zero;
		transform.position = players[id].position + players[id].forward * 12;
	}


    void serviceLaunch(int playerIndex)
    {

		//this.transform.SetParent(null);
		rb.AddForce(players[playerIndex].forward * players[playerIndex].GetComponent<Bounce>().bounciness, ForceMode.Impulse);
	   /* switch (playerIndex)
        {
            case 0:
                this.GetComponent<Rigidbody>().AddForce(this.Player1.forward * this.Player1.GetComponent<Bounce>().bounciness, ForceMode.Impulse);
                break;
            case 1:
                this.GetComponent<Rigidbody>().AddForce(this.Player2.forward * this.Player2.GetComponent<Bounce>().bounciness, ForceMode.Impulse);
                break;
            default:
                break;
        }*/
	}
}
