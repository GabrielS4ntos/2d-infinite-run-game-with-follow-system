using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreManager : MonoBehaviour {
    public Text scoreText;
    public Text highScoreText;
    public Text followersCountText;
    public Text bottleCountText;
    public Text scoreDeathText;
    public Text followersDeathText;

    public float scoreCount;
    public int followersCount;
    private int biggestFollowersCount;
    public int bottlesCount;

    public float highScoreCount;
    public int highFollowersCount;
    public int highBottlesCount;

    public bool scoreIncreasing;
    public float pointsPerSecond;
    private HordeController horde;

    public bool isScoreRecord;
    public bool isBottlesRecord;
    public bool isFollowersRecord;

    public Text scoreRecord;
    public Text bottlesRecord;
    public Text followersRecord;

    // Use this for initialization
    void Start () {
        horde = FindObjectOfType<HordeController>();
        followersCount = 1;
		if(PlayerPrefs.HasKey("HighScore"))
        {
            highScoreCount = PlayerPrefs.GetFloat("HighScore");
        }
        if (PlayerPrefs.HasKey("HighFollowersCount"))
        {
            highFollowersCount = PlayerPrefs.GetInt("HighFollowersCount");
        }
        if (PlayerPrefs.HasKey("HighBottlesCount"))
        {
            highFollowersCount = PlayerPrefs.GetInt("HighBottlesCount");
        }
    }
	
	// Update is called once per frame
	void Update () {

        if(followersCount > biggestFollowersCount)
        {
            biggestFollowersCount = followersCount;
        }

        if (scoreIncreasing)
        {
            scoreCount += (pointsPerSecond * (Time.deltaTime + horde.speed)) * followersCount / 100f;
        }
        if (scoreCount > highScoreCount)
        {
            if(highScoreCount < 0)
            {
                isScoreRecord = true;
            }
            highScoreCount = scoreCount;
            PlayerPrefs.SetFloat("HighScore", highScoreCount);
        }
        if (bottlesCount > highBottlesCount)
        {
            if (highBottlesCount < 0)
            {
                isBottlesRecord = true;
            }
            highBottlesCount = bottlesCount;
            PlayerPrefs.SetFloat("HighBottlesCount", highBottlesCount);
        }

        if (biggestFollowersCount > highFollowersCount)
        {
            if(highFollowersCount == 0)
            {
                isFollowersRecord = false;
            } else
            {
                isFollowersRecord = true;
            }
            highFollowersCount = biggestFollowersCount;
            PlayerPrefs.SetFloat("HighFollowersCount", highFollowersCount);
        }

        followersCountText.text = "" + followersCount;
        scoreText.text = "" + Mathf.Round(scoreCount);
        scoreDeathText.text = "" + Mathf.Round(scoreCount);
        highScoreText.text = "" + Mathf.Round(highScoreCount);
        bottleCountText.text = "" + bottlesCount;
        followersDeathText.text = "" + highFollowersCount;
    }

    public void AddScore(int pointsToAdd)
    {
        scoreCount += pointsToAdd;
    }
}
