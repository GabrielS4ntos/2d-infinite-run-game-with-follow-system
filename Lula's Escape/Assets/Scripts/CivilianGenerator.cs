using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CivilianGenerator : MonoBehaviour {

    public ObjectPooler[] civilianPool;
    private int civilianSelector;

    public float distanceBetweenCivilians;

    public void spawnCivilian(Vector3 startPoint)
    {
        civilianSelector = Random.Range(0, civilianPool.Length);
        GameObject civilian = civilianPool[civilianSelector].GetPooledObject();
        civilian.transform.position = startPoint;
        civilian.SetActive(true);
    }
}
