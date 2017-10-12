using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandBehavior : MonoBehaviour {

    public SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller.Device device;
    private bool isObjectMenuVisible;
    public float throwForce = 1.5f;

    //Swipe
    public float swipeSum;
    public float touchLast;
    public float touchCurrent;
    public float distance;
    public bool hasSwipedLeft;
    public bool hasSwipedRight;
    public ObjectMenuManager objectMenuManager;
    public float rightOffset = 0.7f;
    public float leftOffset = -0.7f;

    private void Awake()
    {
        objectMenuManager.HideMenu();
    }

    // Use this for initialization
    void Start () {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }
	
	// Update is called once per frame
	void Update () {
        device = SteamVR_Controller.Input((int)trackedObj.index);

        //if (device.GetTouch(SteamVR_Controller.ButtonMask.Touchpad))
        //{
        //    touchCurrent = device.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad).x;
        //    distance = touchCurrent - touchLast;
        //    touchLast = touchCurrent;
        //    swipeSum += distance;
        //    if (!hasSwipedRight)
        //    {
        //        if (swipeSum > 0.5f)
        //        {
        //            swipeSum = 0;
        //            SwipeRight();
        //            hasSwipedRight = true;
        //            hasSwipedLeft = false;
        //        }
        //    }
        //    if (!hasSwipedLeft)
        //    {
        //        if (swipeSum < 0.5f)
        //        {
        //            swipeSum = 0;
        //            SwipeLeft();
        //            hasSwipedLeft = true;
        //            hasSwipedRight = false;
        //        }
        //    }
        //}

        if (device.GetTouchUp(SteamVR_Controller.ButtonMask.Touchpad))
        {
            swipeSum = 0;
            touchCurrent = 0;
            touchLast = 0;
            hasSwipedLeft = false;
            hasSwipedRight = false;
            isObjectMenuVisible = false;
            objectMenuManager.HideMenu();
        }

        if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Touchpad))
        {
            isObjectMenuVisible = true;
            objectMenuManager.ShowMenu();
        }

        if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger) && isObjectMenuVisible)
        {
            //Spawn object currently selected by menu
            SpawnObject();
        }

        if (device.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
        {
            touchCurrent = device.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad).x;
            if (touchCurrent > rightOffset)
            {
                objectMenuManager.MenuRight();
            }

            if (touchCurrent < leftOffset)
            {
                objectMenuManager.MenuLeft();
            }
        }
    }

    private void SpawnObject()
    {
        objectMenuManager.SpawnCurrentObject();
    }

    private void SwipeLeft()
    {
        Debug.Log("Swiped Left!");
        objectMenuManager.MenuLeft();
    }

    private void SwipeRight()
    {
        Debug.Log("Swiped Right!");
        objectMenuManager.MenuRight();
    }

    private void OnTriggerStay(Collider collider)
    {
        return;         //Using SteamVR Interaction System to throw objects
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
