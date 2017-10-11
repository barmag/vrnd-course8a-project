using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandBehavior : MonoBehaviour {

    public SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller.Device device;
    public float throwForce = 1.5f;

    // Use this for initialization
    void Start () {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }
	
	// Update is called once per frame
	void Update () {
        device = SteamVR_Controller.Input((int)trackedObj.index);
    }

    private void OnTriggerStay(Collider collider)
    {
        if (collider.CompareTag("Throwable"))   //Interact with throwable objects
        {
            if (device.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
            {
                ThrowObject(collider);
            }

            if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
            {
                GrabObject(collider);
            }
        }
    }

    private void GrabObject(Collider collider)
    {
        collider.GetComponent<Rigidbody>().isKinematic = true;      //Disable gravity
        collider.transform.SetParent(gameObject.transform);         //Attach to controller
        
        device.TriggerHapticPulse(2000);                            //Give indication for holding object
    }

    private void ThrowObject(Collider collider)
    {
        collider.transform.SetParent(null);         //Release object

        var rigidBody = collider.GetComponent<Rigidbody>();
        rigidBody.isKinematic = false;      //enable gravity
        rigidBody.velocity = device.velocity * throwForce;
        rigidBody.angularVelocity = device.angularVelocity;
    }
}
