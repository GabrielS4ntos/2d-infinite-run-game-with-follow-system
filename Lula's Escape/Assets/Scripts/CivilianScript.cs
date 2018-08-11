using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CivilianScript : MonoBehaviour {

    private HordeController horde;
    private Animator civilianAnimator;
	// Use this for initialization
	void Start () {
        horde = FindObjectOfType<HordeController>();
        civilianAnimator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}


    private IEnumerator OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player" || other.gameObject.name == "follower")
        {
            civilianAnimator.SetBool("isDead", true);
            yield return new WaitForSeconds(1);
            gameObject.SetActive(false);
            horde.AddFollower();
        }
    }
}
