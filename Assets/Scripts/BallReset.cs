using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallReset : MonoBehaviour {

    public Transform BallResetPosition;
    public GameObject Floor;

	// Use this for initialization
	void Start () {
        resetBallPosition();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == Floor)
        {
            resetBallPosition();
        }
    }

    private void resetBallPosition()
    {
        //this.transform.position = BallResetPosition.position;
        
        var ballBody = this.gameObject.GetComponent<Rigidbody>();
        ballBody.velocity = Vector3.zero;
        ballBody.angularVelocity = Vector3.zero;
        //TODO: use an editor value instead of hard-coded
        this.transform.position = new Vector3(-0.5f, 1f, -2.65f);
    }
}
