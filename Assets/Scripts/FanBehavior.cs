using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanBehavior : MonoBehaviour {
    [Range(1, 50)]
    public int force = 10;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay(Collider collider)
    {
        Debug.Log("Fan Hit!");

        if (collider.CompareTag("Throwable"))
        {
            collider.gameObject.GetComponent<Rigidbody>().AddForce(-transform.forward * force, ForceMode.Acceleration);
        }
    }
}
