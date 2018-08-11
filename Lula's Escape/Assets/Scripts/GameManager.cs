using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    //public Transform platformGenerator;
    //private Vector3 platformStartPoint;

    public PlayerController player;
    //private Vector3 playerStartPoint;

    //public Transform backgroundGenerator;
    //private Vector3 backgroundStartPoint;

    //private ObjectDestroyer[] objectList;

    private ScoreManager scoreManager;
    public DeathMenu deathMenu;
    
    
    private GameObject UIinGame;
    private GameObject slider;
    public string restartGameLevel;
    // Use this for initialization
    void Start () {
        //horde = FindObjectOfType<HordeController>();
        //platformStartPoint = platformGenerator.position;
        //playerStartPoint = player.transform.position;
        //backgroundStartPoint = backgroundGenerator.position;
        scoreManager = FindObjectOfType<ScoreManager>();
        UIinGame = GameObject.FindGameObjectWithTag("UI");
        slider = GameObject.FindGameObjectWithTag("slider");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void RestartGame()
    {

        player.gameObject.SetActive(false);
        scoreManager.scoreIncreasing = false;
        UIinGame.SetActive(false);
        slider.SetActive(false);
        deathMenu.gameObject.SetActive(true);

        if (scoreManager.isScoreRecord == false)
        {
            scoreManager.scoreRecord.gameObject.SetActive(false);
        }
        else
        {
            scoreManager.scoreRecord.gameObject.SetActive(true);
        }

        if (scoreManager.isBottlesRecord == false)
        {
            scoreManager.bottlesRecord.gameObject.SetActive(false);
        }
        else
        {
            scoreManager.bottlesRecord.gameObject.SetActive(true);
        }

        if (scoreManager.isFollowersRecord == false)
        {
            scoreManager.followersRecord.gameObject.SetActive(false);
        }
        else
        {
            scoreManager.followersRecord.gameObject.SetActive(true);
        }
        //StartCoroutine("RestartGameCo");

    }

    public void Reset()
    {
        deathMenu.gameObject.SetActive(false);
        SceneManager.LoadScene(restartGameLevel);
        
    }

    /*public IEnumerator RestartGameCo()
    {
        scoreManager.scoreIncreasing = false;
        yield return new WaitForSeconds(0.1f);

        SceneManager.LoadScene(restartGameLevel);
      
        objectlist = findobjectsoftype<objectdestroyer>();
        for (int i = 0; i < objectlist.length; i++)
        {
            objectlist[i].gameobject.setactive(false);
        }

        horde.followers = new list<gameobject>();
        scoremanager.followerscount = 1;
        player.transform.position = playerstartpoint;
        platformgenerator.position = platformstartpoint;
        backgroundgenerator.position = backgroundstartpoint;
        player.gameobject.setactive(true);

        scoremanager.scorecount = 0;
        scoremanager.scoreincreasing = true;
    }*/
}
