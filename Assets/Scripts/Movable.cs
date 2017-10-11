using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(Interactable))]
[RequireComponent(typeof(Rigidbody))]

public class Movable : MonoBehaviour {
    private bool attached;
    private float attachTime;
    private Vector3 attachPosition;
    private Quaternion attachRotation;

    public UnityEvent onPickUp;
    public UnityEvent onDetachFromHand;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnAttachedToHand(Hand hand)
    {
        attached = true;

        onPickUp.Invoke();

        hand.HoverLock(null);

        Rigidbody rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        rb.interpolation = RigidbodyInterpolation.None;

        attachTime = Time.time;
        attachPosition = transform.position;
        attachRotation = transform.rotation;
    }


    //-------------------------------------------------
    private void OnDetachedFromHand(Hand hand)
    {
        attached = false;

        onDetachFromHand.Invoke();

        hand.HoverUnlock(null);

        Rigidbody rb = GetComponent<Rigidbody>();
        rb.interpolation = RigidbodyInterpolation.Interpolate;

        Vector3 position = Vector3.zero;
    }
}
