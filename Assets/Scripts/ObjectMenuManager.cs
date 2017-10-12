using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMenuManager : MonoBehaviour {

    public List<GameObject> objectList;
    public List<GameObject> objectPrefabList;
    public GameObject controller;

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

    public void SpawnCurrentObject()
    {
        //Instantiate(objectPrefabList[currentObject],
        //    objectList[currentObject].transform.position,
        //    objectList[currentObject].transform.rotation);
        Vector3 controllerPosition = controller.transform.position;
        float spawnDistance = .3f; // to prevent the prefab from spawning right in front of our face
        var instance = Instantiate(objectPrefabList[currentObject], new Vector3(controllerPosition.x, controllerPosition.y, controllerPosition.z + spawnDistance), objectPrefabList[currentObject].transform.rotation);
        Debug.Log(string.Format("spawn position: {0}", instance.transform.position));

    }
}
