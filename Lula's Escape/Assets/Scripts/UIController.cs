using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    public Image bgTutorialImage;
    public Image jumpTutorialImage;
    public Image hcTutorialImage;
    public Image balloonImage;
    public float waitTime;
    private float time;

    // Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        if (!PlayerPrefs.HasKey("Tutorial"))
        {
            bgTutorialImage.gameObject.SetActive(true);
            jumpTutorialImage.gameObject.SetActive(true);
            hcTutorialImage.gameObject.SetActive(true);

            var tempColor1 = bgTutorialImage.color;
            var tempColor2 = jumpTutorialImage.color;
            var tempColor3 = hcTutorialImage.color;

            tempColor1.a -= Time.deltaTime / 50;
            bgTutorialImage.color = tempColor1;

            tempColor2.a -= Time.deltaTime / 20;
            jumpTutorialImage.color = tempColor2;

            tempColor3.a -= Time.deltaTime / 20;
            hcTutorialImage.color = tempColor3;

            PlayerPrefs.SetString("Tutorial", "Tutorial Watched");
        }
    }

}
