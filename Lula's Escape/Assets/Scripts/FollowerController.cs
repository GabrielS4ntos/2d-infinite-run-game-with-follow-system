using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerController : MonoBehaviour {

    public float speed;
    public float speedIncrementor;
    public float maxSpeed;
    public float jumpForce;
    private Rigidbody2D playerRigidBody;
    public bool grounded;
    public LayerMask whatIsGround;
    public Transform groundCheck;
    public float groundCheckRadius;
    //private Collider2D playerCollider;
    private Animator playerAnimator;
    private ScoreManager scoreManager;
    // Use this for initialization
    public GameManager gameManager;
    public GameObject horde;
    public float distanceBetweenPlayer;
    public bool isInvulnerable;

    void Start()
    {
        isInvulnerable = false;
        scoreManager = FindObjectOfType<ScoreManager>();
        playerRigidBody = GetComponent<Rigidbody2D>();
        //playerCollider = GetComponent<Collider2D>();
        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
   
        if (speed < maxSpeed)
        {
            speed = speed + speedIncrementor;
        }
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        playerRigidBody.velocity = new Vector2(speed, playerRigidBody.velocity.y);

        playerAnimator.SetBool("Grounded", grounded);
    }
    
    public void Jump() {
        if (grounded)
        {
            playerRigidBody.velocity = new Vector3(playerRigidBody.velocity.x, jumpForce, 0);
        }
        playerAnimator.SetBool("Grounded", grounded);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "killbox")
        {
            scoreManager.followersCount--;
            gameObject.SetActive(false);
        }

    }
}
