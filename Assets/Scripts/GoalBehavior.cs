using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalBehavior : MonoBehaviour {

    public string nextLevel;
    public GameObject objectivesRoot;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Throwable"))
        {
            Debug.Log("Goal hit!");
            foreach (Transform t in objectivesRoot.transform)
            {
                var c = t.gameObject.GetComponent<Collectible>();
                if (c != null)
                {
                    if (!c.isCollected)
                    {
                        return;
                    }
                }
            }
            if (!string.IsNullOrEmpty(nextLevel))
            {
                SteamVR_LoadLevel.Begin(nextLevel);
            }
        }
    }
}
