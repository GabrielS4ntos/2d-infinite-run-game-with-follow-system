using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleGenerator : MonoBehaviour
{

    public ObjectPooler bottlePool;

    public float distanceBetweenBottles;

    public void spawnBottles(Vector3 startPoint)
    {
        GameObject bottle1 = bottlePool.GetPooledObject();
        bottle1.transform.position = startPoint;
        bottle1.SetActive(true);
        GameObject bottle2 = bottlePool.GetPooledObject();
        bottle2.transform.position = new Vector3(startPoint.x + distanceBetweenBottles, startPoint.y, startPoint.z);
        bottle2.SetActive(true);
        GameObject bottle3 = bottlePool.GetPooledObject();
        bottle3.transform.position = new Vector3(startPoint.x - distanceBetweenBottles, startPoint.y, startPoint.z);
        bottle3.SetActive(true);
    }
}