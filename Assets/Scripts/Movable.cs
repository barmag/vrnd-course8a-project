using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(Interactable))]
[RequireComponent(typeof(Rigidbody))]

public class Movable : MonoBehaviour {
    [EnumFlags]
    [Tooltip("The flags used to attach this object to the hand.")]
    public Hand.AttachmentFlags attachmentFlags = Hand.AttachmentFlags.ParentToHand | Hand.AttachmentFlags.DetachFromOtherHand;

    [Tooltip("Name of the attachment transform under in the hand's hierarchy which the object should should snap to.")]
    public string attachmentPoint;

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

    private void OnHandHoverBegin(Hand hand)
    {
        Debug.Log("Hand hover!");
    }

    private void HandHoverUpdate(Hand hand)
    {
        //Trigger got pressed
        if (hand.GetStandardInteractionButtonDown())
        {
            hand.AttachObject(gameObject, attachmentFlags, attachmentPoint);
            ControllerButtonHints.HideButtonHint(hand, Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger);
        }
    }

    private void OnAttachedToHand(Hand hand)
    {
        Debug.Log("Hand attached!");
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

    private void HandAttachedUpdate(Hand hand)
    {
        Debug.Log("Attach update!");
        //Trigger got released
        if (!hand.GetStandardInteractionButton())
        {
            // Detach ourselves late in the frame.
            // This is so that any vehicles the player is attached to
            // have a chance to finish updating themselves.
            // If we detach now, our position could be behind what it
            // will be at the end of the frame, and the object may appear
            // to teleport behind the hand when the player releases it.
            StartCoroutine(LateDetach(hand));
        }
    }

    private void OnHandFocusAcquired(Hand hand)
    {
        Debug.Log("Hand focused");
        gameObject.SetActive(true);
    }

    private void OnHandFocusLost(Hand hand)
    {
        Debug.Log("Hand lost focus");
        gameObject.SetActive(false);
    }

    private IEnumerator LateDetach(Hand hand)
    {
        yield return new WaitForEndOfFrame();

        hand.DetachObject(gameObject, false);
    }
}
