using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]

public class MilaController : MonoBehaviour
{
    public float distance;
    public static MilaController instance;
    private GameManager gameManager;
    
    
    //getting things
    private Rigidbody rigidBody;
    public Vector3 movement;
    private Animator MilaAnimator;

    //Scripts-----------------------------------
    public SceneController sceneController;
    public CameraController cameraController;

    //setting variables
    [Header ("Movement")]
    //-------------------------------
    
    public bool isRunning;
    public bool isCrawling;
    public int runSpeed;
    public int crawlSpeed;
    private int speed;

    [Header ("vertical Movement")]
    public bool isRoof;
    public bool isLadder1;
    public bool isLadder2;
    public int jumpHeight;
    public float jumpDelay;
    private float jumpTimer;
    public float fallDamageTime;
    
    //How long is Mila are allowed to be in air before dmg
    public float doFallDamage;
    //If Mila receives damage how much more airtime until High damage
    public float highFallHeight;
    
    public float highFallDamage;
    public float lowFallDamage;

    private bool fallingNow;

    [Header ("Detectors")]
    public bool isGrounded1;
    public bool isGrounded2;
    public bool isJumping;
    public float groundLenght;
    public float roofLenght;

    [Header("Detection")]
    public bool inAir;
    public LayerMask groundLayer;
    public LayerMask ladderLayer;
    public Vector3 groundColliderOffset1;
    public Vector3 groundColliderOffset2;
    public Vector3 frontOffest;

    [Header("GameManagement")]
    public int scene = 0;

    void Start()
    {
        instance = this;
        DontDestroyOnLoad(this.gameObject);
        rigidBody = GetComponent<Rigidbody>();
        MilaAnimator = GetComponent<Animator>();
        gameManager = GetComponent<GameManager>();
        MilaAnimator.SetBool("isRunning",isRunning);
        
        isRunning = false;
        isJumping = false;
        isGrounded1 = true;
        isGrounded2 = true;
        isCrawling = false;
        isRoof = false;
        isLadder1 = false;
        isLadder2 = false;
        normalFreeze();
    }

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            jumpTimer = Time.time + jumpDelay;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            
        }
    }
    private void FixedUpdate()
    {
        MilaAnimator.SetBool("isRunning", false);
        speed = runSpeed;

        // collision detection ------------
        // two groundColliderOffest because reusing Groundcollideroffest worked on  
        isGrounded1 = Physics.Raycast(transform.position + groundColliderOffset1, Vector3.down, groundLenght, groundLayer);
        isGrounded2 = Physics.Raycast(transform.position - groundColliderOffset2, Vector3.down, groundLenght, groundLayer);

        isRoof = Physics.Raycast(transform.position, Vector3.up, roofLenght, groundLayer);

        isLadder1 = Physics.Raycast(transform.position + frontOffest, Vector3.forward, groundLenght, ladderLayer);
        isLadder2 = Physics.Raycast(transform.position - frontOffest, Vector3.back, groundLenght, ladderLayer);

        // ladders 
        if (isLadder1 || isLadder2)
        {
            MilaAnimator.SetBool("onLadder", true);
            speed = crawlSpeed;
            rigidBody.useGravity = false;
            ladderFreeze();
            inAir = false;
        } 
        else
        {
            MilaAnimator.SetBool("onLadder", false);
            speed = runSpeed;
            MilaAnimator.SetBool("isClimbingU", false);
            MilaAnimator.SetBool("isClimbingD", false);
            rigidBody.useGravity = true;
            normalFreeze();

        }
        if ((isLadder1 && Input.GetKey(KeyCode.W)) || (isLadder2 && Input.GetKey(KeyCode.W)))
        {
            MoveUp();
            MilaAnimator.SetBool("isClimbingU", true);
        }
        else
        {
            MilaAnimator.SetBool("isClimbingU", false);
        }
        if ((isLadder1 && Input.GetKey(KeyCode.S)) || (isLadder2  && Input.GetKey(KeyCode.S)))
        {
            MoveDown();
            MilaAnimator.SetBool("isClimbingD", true);
        }
        else
        {
            MilaAnimator.SetBool("isClimbingD", false);
        }
        // Grounded check and jump
        if (isGrounded1 || isGrounded2){
            MilaAnimator.SetBool("isJumping", false);

            if (inAir && fallDamageTime < Time.time)
            {

                float fallTime = Time.time - fallDamageTime;

                if (fallTime > highFallHeight)
                {
                    HealthManager.instance.TakeDamage(highFallDamage);      
                    Debug.Log("High Damage");
                }
                else
                {
                    HealthManager.instance.TakeDamage(lowFallDamage);
                    Debug.Log("Low Damage");
                    Debug.Log(fallTime);
                }
            }
            inAir = false;

        }
        // fall and timer
        if ((jumpTimer > Time.time && isGrounded1) || (jumpTimer > Time.time && isGrounded2))
        {
            Jump();
            //debugging points and lives
        }
        if (!isGrounded1 && !isGrounded2 && !isLadder1 && !isLadder2)
        {
            if (!inAir)
            {
                fallDamageTime = Time.time + doFallDamage;
                inAir = true;
            }

        }

        //crawling and roof check
        if (Input.GetKey(KeyCode.A) && isCrawling == false)
        {
            MoveLeft();
            MilaAnimator.SetBool("isRunning", true);
        }

        if (Input.GetKey(KeyCode.D) && isCrawling == false)
        {
            MoveRight();
            MilaAnimator.SetBool("isRunning", true);
        }

        
        if ((Input.GetKey(KeyCode.S) && isGrounded1) || (Input.GetKey(KeyCode.S) && isGrounded2))
        {
            MilaAnimator.SetBool("crawl", true);
            isCrawling = true;

        }
        else if (isRoof)
        {
            MilaAnimator.SetBool("crawl", true);
            isCrawling = true;
        }
        else
        {
            isCrawling = false;
            MilaAnimator.SetBool("isCrawling", false);
            MilaAnimator.SetBool("crawl", false);
        }

        if (Input.GetKey(KeyCode.W))
        {

        }

        if (isCrawling)
        {
            speed = crawlSpeed;
            inAir = false;
        }

        if (Input.GetKey(KeyCode.A) && isCrawling)
        {
            MoveLeft();
            MilaAnimator.SetBool("isCrawling", true);
            isCrawling = true;
        }
        else if (Input.GetKey(KeyCode.D) && isCrawling)
        {
            MoveRight();
            MilaAnimator.SetBool("isCrawling", true);
            isCrawling = true;
        }
        else
        {
            isCrawling = false;
            MilaAnimator.SetBool("isCrawling", false);
        }

        //Interacting--------------------------------
        if (Input.GetKey(KeyCode.I))
        {
            //Interacting();
        }
    }

    void normalFreeze()
    {
        rigidBody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
    }
    void ladderFreeze()
    {
        rigidBody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
    }


    void Jump()
    {
        MilaAnimator.SetTrigger("takeOf");
        MilaAnimator.SetBool("isJumping", true);
        rigidBody.AddForce(new Vector3(0, jumpHeight, 0), ForceMode.Impulse);
        jumpTimer = 0;
    }

    void MoveRight()
    {
        this.gameObject.transform.rotation = Quaternion.Euler(0f, 0, 0f);
        this.gameObject.transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
    void MoveLeft()
    {
        this.gameObject.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        this.gameObject.transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
    void MoveUp()
    {
        this.gameObject.transform.Translate(Vector3.up * speed * Time.deltaTime);
    }
    void MoveDown()
    {
        this.gameObject.transform.Translate(Vector3.down * speed * Time.deltaTime);
    }

    // Collision Debuggs----------------------
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position + groundColliderOffset1 , transform.position + groundColliderOffset1 + Vector3.down * groundLenght);
        Gizmos.DrawLine(transform.position - groundColliderOffset2, transform.position - groundColliderOffset2 + Vector3.down * groundLenght);
        Gizmos.DrawLine(transform.position, transform.position + Vector3.up * roofLenght);
        Gizmos.DrawLine(transform.position + frontOffest, transform.position + frontOffest + Vector3.forward * groundLenght);
        Gizmos.DrawLine(transform.position - frontOffest, transform.position - frontOffest - Vector3.forward * groundLenght);

    }

    public void TelePort()
    {
        transform.position = new Vector3(0.0f,-15f,35.5f);
    }

    public void ChangeScene(float currentHealth)
    {
        if (scene == 0)
        {
            GameManager.instance.SetStartHealth();
            SceneManager.LoadScene("DreamScene");
            scene++;
        }
        else if (scene == 1 && currentHealth <= 6.0f)
        {
            SceneManager.LoadScene("NightmareScene");
            scene++;
        }
        else if (scene == 2 && currentHealth >= 7.0f)
        {
            SceneManager.LoadScene("DreamScene");
            scene--;
        }
    }
}
