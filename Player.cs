using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //public variables
    public static float speed=3;
    public float hKnockback;
    public float vKnockback;

    //private variables
    private bool isGrounded;
    public static float moveInput;
    private bool facingRight=false;
    private float jumpTimer = 0.1f;
    private float jumpTimerDefault = 0;
    private float groundedTimerDefault = 0;
    private float groundedTimer = 0.1f;
    private int currentHealth;
    private bool leftKnockback;
    private float knockbackCount;

    //public components
    public LayerMask platform;
    public Transform groundCheck;
    public float checkRadius;
    public float jumpForce;
    public GameObject coinPlatform;
    public Transform coinSpawn;
    public GameObject coinAlert;
    public GameObject dmgedScreen;
    public GameObject gameOverScreen;
    public GameObject coinParticles;
    public GameObject cameraPlayer;

    //private components
    private Rigidbody2D rigid;
    private Animator anim;
    private Health healthScript;
    private Player playerScript;


    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        healthScript = GetComponent<Health>();
        currentHealth = healthScript.GetHealth();
        playerScript = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        currentHealth = healthScript.GetHealth();
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, platform); //checks if player is touching the ground
        groundedTimerDefault -= Time.deltaTime; 
        jumpTimerDefault -= Time.deltaTime;
        if (isGrounded)
        {
            groundedTimerDefault = groundedTimer;
        }

        


        if (Input.GetButtonDown("Jump"))
        {
            jumpTimerDefault = jumpTimer;
        }

        if ((jumpTimerDefault > 0) && groundedTimerDefault > 0)
        {
            //play jump sound
            transform.parent = null;
            groundedTimerDefault = 0;
            jumpTimerDefault = 0;
            FindObjectOfType<AudioManager>().Play("jump"); //play jumping sound
           
            rigid.velocity = Vector2.up * jumpForce; //player jumps
            anim.SetBool("isJumping", true); //play jumping animation
        }

        if (Input.GetButtonUp("Jump"))
        {
            if (rigid.velocity.y > 0)
            {
                //player is going upwards, cut velocity in half (short jump)
                rigid.velocity = Vector2.up * 0.5f;
            }
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            //check if player HP is > 1: if true, subtract 1 hp, and spawn a coin platform. Else, play error noise, and flash player health.
            if (currentHealth > 0)
            {
                TakeDamage(1);
                FindObjectOfType<AudioManager>().Play("dropBar");
                SpawnCoin();
            }
            else
            {
                //flash (Need Coins) above player head for x seconds
                FindObjectOfType<AudioManager>().Play("needCoin");
                StartCoroutine(NeedCoins());
            }
            

        }
    }


    IEnumerator NeedCoins()
    {
        coinAlert.SetActive(true);
        //turn off after 2 seconds
        yield return new WaitForSeconds(2.0f);
        coinAlert.SetActive(false);
    }



    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Platform") || other.gameObject.CompareTag("GoldBar") || other.gameObject.CompareTag("Moving Platform"))
        {
            
            anim.SetBool("isJumping", false);
        }

        if(other.gameObject.CompareTag("Grunt"))
        {
            //lower health by 1
            TakeDamage(1);
            //knock back player
            //if hit from the right side knockback left
            //check contact points of other
            foreach(ContactPoint2D hitPosition in other.contacts)
            {
                if (hitPosition.normal.x < 0)
                {
                    //hit from right, go left
                    leftKnockback = true;
                }

                else if (hitPosition.normal.x > 0)
                {
                    leftKnockback = false;
                }
            }
            
            //set knockback amount
            knockbackCount = 0.2f;
            PlayerHurt();
            
        }

        if (other.gameObject.CompareTag("Coin"))
        {
            //pick up Coin, destroy coin object
            GainHealth();

            FindObjectOfType<AudioManager>().Play("pickupCoin");
            Destroy(other.gameObject);
        }
    }


    void FixedUpdate()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        
        rigid.velocity = new Vector2(moveInput * speed, rigid.velocity.y);
        anim.SetFloat("speed",Mathf.Abs(moveInput));
        //flip character sprite in direction facing
        if (facingRight && moveInput > 0)
        {
            Flip();
        }
        else if (!facingRight && moveInput < 0)
        {
            Flip();
        }

        //take into account knockback situation
        if (knockbackCount <= 0)
        {
            //only able to move when not in damaged state
            rigid.velocity = new Vector2(moveInput * speed, rigid.velocity.y);
        }
        else
        {
            if (leftKnockback)
            {
                // go left
                rigid.velocity=(new Vector2(-hKnockback, vKnockback));
            }
            if (!leftKnockback)
            {
                rigid.velocity=(new Vector2(hKnockback, vKnockback));
            }
            knockbackCount -= Time.deltaTime;
        }
    }


    private void Flip()
    {
        //flip sprite if player is facing opposite direction
        facingRight = !facingRight;

        transform.Rotate(0f,180f,0f);
        //inverse flip "need coins" text to prevent backwards text
        coinAlert.transform.Rotate(0f,180f,0f);
    }

    private void PlayFootstep()
    {
        FindObjectOfType<AudioManager>().Play("footsteps");
    }

    private void SpawnCoin() //call this only if player has sufficient HP
    {
        Instantiate(coinPlatform, coinSpawn.position, coinSpawn.rotation);
    }


    /* DO INTERACTIONS HERE (W/ PLAYER HP, ENEMIES, ETC) */
    private void TakeDamage(int dmg)
    {
        if (currentHealth == 0)
        {
            //game is over
            Debug.Log("gameover");
            GameOver();
        }
        else
        {
           healthScript.SetHealth(currentHealth - dmg);
        }
        
    }

    private void GainHealth()
    {
        healthScript.SetHealth(currentHealth + 1);
    }


    /* ALL ANIMATIONS / PARTICLES / GRAPHICS / SOUNDS WHEN PLAYER TAKES DAMAGE */
    public void PlayerHurt()
    {
        //play hurt audio
        FindObjectOfType<AudioManager>().Play("damaged");
        //Remove player control for duration of knockback

        // darken screen for duration of knockback count
        StartCoroutine(ScreenDuration(knockbackCount));
    }

    IEnumerator ScreenDuration(float duration)
    {
        dmgedScreen.SetActive(true);
        //anim.SetBool("isHurt", true);
        yield return new WaitForSeconds(duration);
        //anim.SetBool("isHurt", false);
        dmgedScreen.SetActive(false);
    }

    private void GameOver()
    {
        //remove player control from character
        StartCoroutine(DestroyPlayer());
    }

    IEnumerator DestroyPlayer()
    {
        Instantiate(coinParticles, transform.position, transform.rotation);
        Destroy(gameObject);
        gameOverScreen.SetActive(true);
        cameraPlayer.SetActive(false);
        yield return new WaitForSeconds(1f);
        playerScript.enabled = false;
    }
}
