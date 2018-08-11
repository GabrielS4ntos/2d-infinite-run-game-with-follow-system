using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundGeneration : MonoBehaviour
{

    private GameObject background;
    public Transform generationPoint;

    private float backgroundWidth;

    //public GameObject[] platforms;
    //private int backgroundSelector;
    private float[] backgroundsWidths;

    public ObjectPooler[] objectPools;

    // Use this for initialization
    void Start()
    {
        //platformWidth = platform.GetComponentInChildren<BoxCollider2D>().size.x;
        backgroundsWidths = new float[objectPools.Length];
        for (int i = 0; i < objectPools.Length; i++)
        {
            backgroundsWidths[i] = 60.48f;//objectPools[i].pooledObject.GetComponentInChildren<BoxCollider2D>().size.x;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < generationPoint.position.x)
        {
            transform.position = new Vector3(transform.position.x + backgroundsWidths[0], transform.position.y, transform.position.z);

            //Instantiate(platforms[platformSelector], transform.position, transform.rotation);

            GameObject newBackground = objectPools[0].GetPooledObject();
            newBackground.transform.position = transform.position;
            newBackground.transform.rotation = transform.rotation;
            newBackground.SetActive(true);
        }
    }
}
