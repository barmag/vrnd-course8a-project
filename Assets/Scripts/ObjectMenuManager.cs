using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMenuManager : MonoBehaviour {

    public List<GameObject> objectList;
    public List<GameObject> objectPrefabList;

    public int currentObject = 0;

	// Use this for initialization
	void Start () {
        foreach (Transform child in this.transform)
        {
            objectList.Add(child.gameObject);
        }
	}
	
	public void MenuLeft()
    {
        objectList[currentObject].SetActive(false);

        currentObject = currentObject == 0 ? objectList.Count - 1 : currentObject - 1;
        objectList[currentObject].SetActive(true);
    }

    public void MenuRight()
    {
        objectList[currentObject].SetActive(false);
        currentObject = currentObject == objectList.Count - 1 ? 0 : currentObject + 1;
        objectList[currentObject].SetActive(true);
    }
}
