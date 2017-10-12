using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour {
    public bool isCollected = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    internal void RestCollectible()
    {
        Debug.Log("Resetting collectibles");
        isCollected = false;
        gameObject.SetActive(true);
    }

    internal void Collect()
    {
        isCollected = true;
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Throwable"))
        {
            Debug.Log("Collectible collision triggered");
            Collect();
        }
        
    }
}
