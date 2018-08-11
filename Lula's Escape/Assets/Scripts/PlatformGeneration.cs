using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGeneration : MonoBehaviour {

    private GameObject platform;
    public Transform generationPoint;
    public float distanceBetween;

    private float platformWidth;

    public float distanceBetweenMin;
    public float distanceBetweenMax;

    //public GameObject[] platforms;
    private int platformSelector;
    private float[] platformsWidths;

    public ObjectPooler[] objectPools;

    //Random Height
    private float minHeight;
    public Transform maxHeightPoint;
    private float maxHeight;
    public float maxHeightChange;
    private float heightChange;

    //Hole width incrementor
    public GameObject player;
    private float holeIncrementor;

    private BottleGenerator bottleGenerator;
    public float randomBottle;

    private CivilianGenerator civilianGenerator;
    public float randomCivilian;

    public float randomEnemy;
    public ObjectPooler[] enemyPools;
    private int randomEnemyNumber;
    // Use this for initialization
    void Start () {
        //platformWidth = platform.GetComponentInChildren<BoxCollider2D>().size.x;
        platformsWidths = new float[objectPools.Length];
        for(int i = 0; i < objectPools.Length; i++)
        {
            platformsWidths[i] = objectPools[i].pooledObject.GetComponentInChildren<BoxCollider2D>().size.x;
        }

        minHeight = transform.position.y;
        maxHeight = maxHeightPoint.position.y;

        bottleGenerator = FindObjectOfType<BottleGenerator>();
        civilianGenerator = FindObjectOfType<CivilianGenerator>();

    }
	
	// Update is called once per frame
	void Update () {

        holeIncrementor = player.GetComponent<PlayerController>().speed * 0.1f;



        if (transform.position.x < generationPoint.position.x)
        {
            distanceBetween = Random.Range(distanceBetweenMin, distanceBetweenMax);

            distanceBetween = distanceBetween + holeIncrementor;

            platformSelector = Random.Range(0, objectPools.Length);

            heightChange = transform.position.y + Random.Range(maxHeight, -maxHeight);

            if (heightChange > maxHeight)
            {
                heightChange = maxHeight;
            }
            else if (heightChange < minHeight)
            {
                heightChange = minHeight;
            }

            transform.position = new Vector3(transform.position.x + platformsWidths[platformSelector] + distanceBetween, heightChange, transform.position.z);

            //Instantiate(platforms[platformSelector], transform.position, transform.rotation);

            GameObject newPlatform = objectPools[platformSelector].GetPooledObject();
            newPlatform.transform.position = transform.position;
            newPlatform.transform.rotation = transform.rotation;
            newPlatform.SetActive(true);

            // Putting bottle in the platform based on percentage;
            if (Random.Range(0f, 100f) < randomBottle)
            {
                bottleGenerator.spawnBottles(new Vector3(transform.position.x + Random.Range(-10f,10f), transform.position.y + Random.Range(5f,6f), transform.position.z -1f));
            }

            //Putting civilian in the platform...
            if ( Random.Range(0f, 100f) < randomCivilian)
            {
                civilianGenerator.spawnCivilian(new Vector3(transform.position.x + Random.Range(3f, 8f), transform.position.y + 2f, transform.position.z - 1f));
            }
            //Putting enemy in the platform...
            if ( Random.Range(0f, 100f) < randomEnemy)
            {
                randomEnemyNumber = Random.Range(0, 2);
                GameObject newEnemy = enemyPools[randomEnemyNumber].GetPooledObject();

                Vector3 enemyPosition = new Vector3(Random.Range(distanceBetweenMin, distanceBetweenMax),2f,-1f);

                newEnemy.transform.position = transform.position + enemyPosition;
                newEnemy.transform.rotation = transform.rotation;
                newEnemy.SetActive(true);

            }
        }
	}
}
