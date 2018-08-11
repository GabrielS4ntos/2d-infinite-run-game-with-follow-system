using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDestroyer : MonoBehaviour {

    private GameObject objectsDestructionPoint;
	// Use this for initialization
	void Start () {
        objectsDestructionPoint = GameObject.Find("ObjectsDestructionPoint");
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.x < objectsDestructionPoint.transform.position.x)
        {
            //Destroy(gameObject);
            gameObject.SetActive(false);
        }
	}
}
