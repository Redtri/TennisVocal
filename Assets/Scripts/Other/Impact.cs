using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Impact : MonoBehaviour
{
    public GameObject toSpawn;

    private void OnCollisionEnter(Collision collision) {
        GameObject GO = Instantiate(toSpawn, this.gameObject.transform.position, toSpawn.transform.rotation, null);

        GO.transform.localScale = new Vector3(20,20,20);

        Destroy(GO, .05f);
    }
}
