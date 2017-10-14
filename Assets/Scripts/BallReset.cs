using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class BallReset : MonoBehaviour {

    public Transform BallResetPosition;
    public GameObject Floor;
    public GameObject ObjectivesRoot;

    public Material disabledMaterial;
    public Material enabledMaterial;

	// Use this for initialization
	void Start () {
        resetBallPosition();
        Valve.VR.InteractionSystem.Teleport.Player.AddListener(playerTeleported);
    }

    private void playerTeleported(TeleportMarkerBase marker)
    {
        if (marker.CompareTag("Platform"))
        {
            Debug.Log("teleported to platform, enable ball");
            gameObject.GetComponent<Renderer>().material = enabledMaterial;
            gameObject.tag = "Throwable";
        }
        else
        {
            Debug.Log("teleported out, disable ball");
            gameObject.GetComponent<Renderer>().material = disabledMaterial;
            gameObject.tag = "disabled_ball";
        }
    }

    // Update is called once per frame
    void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == Floor)
        {
            resetBallPosition();
            resetCollectibles();
        }
    }

    private void resetCollectibles()
    {
        foreach (Transform t in ObjectivesRoot.transform)
        {
            var c = t.gameObject.GetComponent<Collectible>();
            if (c != null)
            {
                c.RestCollectible();
            }

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
