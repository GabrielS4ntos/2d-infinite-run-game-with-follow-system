using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {

    private PlayerController playerController;
    private HordeController hordeController;
    private GameManager gameManager;
	// Use this for initialization
	void Start () {
        playerController = FindObjectOfType<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && playerController.isInvulnerable == false)
        {
            playerController.isDead();
        }
    }
}
