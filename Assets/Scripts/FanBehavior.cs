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

    private void OnCollisionEnter(Collision collision)
    {
        
        Debug.Log("Fan Hit!");

        if (collision.gameObject.CompareTag("Throwable"))
        {
            var dir = collision.contacts[0].point - transform.position;
            var fanForce = -dir.normalized * force;
            Debug.Log(string.Format("fan force {0}", fanForce));
            collision.gameObject.GetComponent<Rigidbody>().AddForce(fanForce, ForceMode.VelocityChange);
        }
    }
}
