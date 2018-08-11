using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;


public class HordeController : MonoBehaviour {
    public float speed;
    public float speedIncrementor;
    public float maxSpeed;
    public float timeDelay;
    public ObjectPooler[] followersPool;
    private int followerSelector;
    public List<GameObject> followers;
    public List<GameObject> activeFollowers;
    private float distanceBetweenFollowers;
    private float heightFollower;
    private int indexFollower;
    private ScoreManager scoreManager;
    private int activeFollowersNumber;
    private Vector3 newFollowerPoint;
    private GameObject player;
    private float waitTime;
    public Slider slider;

    private float sqrMag1;
    private float sqrMag2;
    // Use this for initialization
    void Start () {
        slider.value = 5f;
        player = GameObject.FindGameObjectWithTag("Player");
        scoreManager = FindObjectOfType<ScoreManager>();
        waitTime = 0.2f;
    }
	
	// Update is called once per frame
	void Update () {
        //when pressed, player will be invulnarable 
        if (CrossPlatformInputManager.GetButton("Fire1") && slider.value >= 0)
        {
            slider.value -= Time.deltaTime;

            if (activeFollowers.Count > 0)
            {
                for (int i = 0; i <= activeFollowers.Count; i++)
                {
                    activeFollowers[i].GetComponent<FollowerController>().isInvulnerable = true;
                    player.GetComponent<PlayerController>().isInvulnerable = true;
                }
            }
            else
            {
                player.GetComponent<PlayerController>().isInvulnerable = true;
            }    
        }
        else if (slider.value <= slider.maxValue)
        {
            slider.value += Time.deltaTime/3;
        } 

        if (CrossPlatformInputManager.GetButtonUp("Fire1") || slider.value == 0)
        {
            player.GetComponent<PlayerController>().isInvulnerable = false;
        }

        if (speed < maxSpeed)
        {
            speed = speed + speedIncrementor;
        }

        DelayJump();
        
        if (speed < 7.5f)
        {
            waitTime = 0.2f;
        }
        else if(speed > 7.5 && speed < 10.5)
        {
            waitTime = 0.15f;
        } else
        {
            waitTime = 0.1f;
        }

    }

   public void AddFollower()
    {
        //Add new follower
        newFollowerPoint = player.transform.position;
        if ( activeFollowersNumber < 14)
        {
  
            followerSelector = Random.Range(0, followersPool.Length);

            GameObject newFollower = followersPool[followerSelector].GetPooledObject();
            scoreManager.followersCount++;
            heightFollower = GetComponentInChildren<CapsuleCollider2D>().size.x;


            followers.Add(newFollower);

            indexFollower = getActiveFollowers().Count;

            distanceBetweenFollowers = heightFollower * indexFollower;
            newFollower.GetComponent<FollowerController>().distanceBetweenPlayer = distanceBetweenFollowers;
            newFollower.transform.position = new Vector3(newFollowerPoint.x - distanceBetweenFollowers - 1f, newFollowerPoint.y, newFollowerPoint.z);
            newFollower.transform.rotation = transform.rotation;
            newFollower.GetComponent<FollowerController>().speed = speed;
            newFollower.SetActive(true);
     
           
        }
        activeFollowersNumber = getActiveFollowers().Count;
    }

    public List<GameObject> getActiveFollowers()
    {
        List<GameObject> activeFollowersList = new List<GameObject>(GameObject.FindGameObjectsWithTag("follower"));
        return activeFollowersList;
    }

    //sort followers by distance to jump according to the order of followers
    public List<GameObject> getSortedActiveFollowers()
    {
        List<GameObject> sortedActiveFollowersList = new List<GameObject>();
        sortedActiveFollowersList = getActiveFollowers();

        for(int i = 0; i < sortedActiveFollowersList.Count - 1; i++)
        {
            sqrMag1 = (player.transform.position - sortedActiveFollowersList[i].transform.position).sqrMagnitude;
            sqrMag2  = (player.transform.position - sortedActiveFollowersList[i + 1].transform.position).sqrMagnitude;

            if (sqrMag1 > sqrMag2)
            {
                GameObject tempStore = sortedActiveFollowersList[i];
                sortedActiveFollowersList[i] = sortedActiveFollowersList[i + 1];
                sortedActiveFollowersList[i + 1] = tempStore;
                i = 0;
            }
        }

        return sortedActiveFollowersList;
    }

    //Delay to followers jump one after another
    public void DelayJump()
    {
        StartCoroutine("DelayJumpCo");
    }

    public IEnumerator DelayJumpCo()
    {
        List<GameObject> sortedActiveFollowersList = new List<GameObject>();
        sortedActiveFollowersList = getSortedActiveFollowers();
        if (CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            for (int i = 0; i < sortedActiveFollowersList.Count; i++)
            {
                sortedActiveFollowersList = getSortedActiveFollowers();
                if (sortedActiveFollowersList[i].activeInHierarchy)
                {
                    yield return new WaitForSeconds(waitTime);
                    sortedActiveFollowersList[i].GetComponent<FollowerController>().Jump();
                }
                sortedActiveFollowersList = getSortedActiveFollowers();
            }
        }
    }


}
