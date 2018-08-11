using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
public class PlayerController : MonoBehaviour
{

    public float speed;

    public float jumpForce;
    private Rigidbody2D playerRigidBody;
    public bool grounded;
    public LayerMask whatIsGround;
    public Transform groundCheck;
    public float groundCheckRadius;
    private Animator playerAnimator;
    //private float timeSpeedIncrementor;
    public float jumpTime;
    private float jumpTimeCounter;
    private bool stoppedJumping;
    // Use this for initialization
    public GameManager gameManager;
    public GameObject horde;
    public bool isInvulnerable;

    void Start()
    {

        playerRigidBody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();

        jumpTimeCounter = jumpTime;
        stoppedJumping = true;
    }

    // Update is called once per frame
    void Update()
    {
        speed = horde.GetComponent<HordeController>().speed;
        //jump
        //grounded = Physics2D.IsTouchingLayers(playerCollider, whatIsGround);

        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        playerRigidBody.velocity = new Vector2(speed, playerRigidBody.velocity.y);
        if (CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            if (grounded)
            {
                playerRigidBody.velocity = new Vector3(playerRigidBody.velocity.x, jumpForce, 0);
                stoppedJumping = false;
            }
        }

        if (CrossPlatformInputManager.GetButton("Jump") && !stoppedJumping)
        {
            if (jumpTimeCounter > 0)
            {
                playerRigidBody.velocity = new Vector3(playerRigidBody.velocity.x, jumpForce, 0);
                jumpTimeCounter -= Time.deltaTime;
            }
        }

        if (CrossPlatformInputManager.GetButtonUp("Jump"))
        {
            jumpTimeCounter = 0;
            stoppedJumping = true;
        }

        if (grounded)
        {
            jumpTimeCounter = jumpTime;
        }

        playerAnimator.SetBool("Grounded", grounded);


    }

    public void isDead()
    {
        horde.gameObject.GetComponent<HordeController>().speed = 4f;
        gameObject.SetActive(false);
        gameManager.RestartGame();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "killbox")
        {
            isDead();
        }

       
    }
}
