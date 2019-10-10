using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Service : MonoBehaviour
{

    [SerializeField] private Transform Player1;
    [SerializeField] private Transform Player2;



    void OnEnable()
    {
        GameManager.instance.onService += serviceStart;
    }

    void OnDisable()
    {
        GameManager.instance.onService -= serviceStart;
    }

    // Start is called before the first frame update
    void Start()
    {
        this.serviceLaunch(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


     void serviceStart(int playerIndex)
    {
        switch (playerIndex)
        {
            case 1:
                this.transform.SetParent(Player1);
                
                break;
            case 2:
                this.transform.SetParent(Player2);
                break;
            default:
                break;
        }
        this.transform.localPosition = new Vector3(0, 0, 2);
    }


    void serviceLaunch(int playerIndex)
    {
        this.transform.SetParent(null);
        switch (playerIndex)
        {
            case 1:
                this.GetComponent<Rigidbody>().AddForce(this.Player1.forward * this.Player1.GetComponent<Bounce>().bounciness, ForceMode.Impulse);
                break;
            case 2:
                this.GetComponent<Rigidbody>().AddForce(this.Player2.forward * this.Player2.GetComponent<Bounce>().bounciness, ForceMode.Impulse);
                break;
            default:
                break;
        }
    }
}
