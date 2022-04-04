using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] LocalCache cache;
    [SerializeField] Text text;
    [SerializeField] float walkSpeed;
    public float runSpeed = 6;
    [SerializeField] float jumpForce;
    [SerializeField] float laneDistance;
    [SerializeField] List<GameObject> skins = new List<GameObject>();
    [SerializeField] GameObject menuElements;

    public bool isGameStarted = false;
    public bool isGamePaused = false;
    public float resumeSpeed;
    float verticalVelocity = -0.1f;
    public float originalSpeed;
    float gravity = 10f;
    float modifier;
    int currentLane = -1;
    bool isSliding = false;
    public bool gameOver = false;
    MySwipe swipe;
    CharacterController controller;
    GameManager gameManager;
    Vector3 moveVector = Vector3.zero;
    bool nextJump = false;

    public Animator anim;
    bool isGrounded = true;

    //Modifiers
    public float speedIncreaseLastTick = 0;
    float speedIncreaseTime = 15f;
    float speedIncreaseAmount = 0.1f;

    void Start()
    {
        originalSpeed = runSpeed;
        modifier = runSpeed;
        swipe = GetComponent<MySwipe>();
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        int skinID = cache.GetSkinID();
        SetSkin(skinID);
    }   

    void Update()
    {
        Old();
    }

    public void Old()
    {
        if (!isGameStarted)
        {
            anim.SetBool("isRunning", false);
            return;
        }

        if(swipe.swipeUp && !isGrounded)
        {
            nextJump = true;
        }

        if (isGamePaused)
        {
            if (swipe.enabled)
                swipe.enabled = false;
        }
        else
        {
            if (!swipe.enabled)
                swipe.enabled = true;
        }

        if (Time.time - speedIncreaseLastTick > speedIncreaseTime)
        {
            speedIncreaseLastTick = Time.time;
            if (runSpeed <= 11 && !gameOver)
                runSpeed += speedIncreaseAmount;
            if (!gameOver && modifier - originalSpeed <= 11)
            {
                modifier += speedIncreaseAmount;
                gameManager.UpdateModifier(modifier - originalSpeed);
            }
        }

        if (swipe.swipeLeft && currentLane != -1)
        {
            MoveLane(false);
        }
        else if (swipe.swipeRight && currentLane != 1)
        {
            MoveLane(true);
        }

        Vector3 targetPosition = transform.position.z * Vector3.forward;

        if (currentLane == -1)
        {
            targetPosition += Vector3.left * laneDistance;
        }
        else if (currentLane == 1)
        {
            targetPosition += Vector3.right * laneDistance;
        }


        moveVector.x = (targetPosition - transform.position).normalized.x * runSpeed;
         
        if (isGrounded)
        {
            isGrounded = false;
            verticalVelocity = -0.1f;
            anim.SetBool("isJumping", false);

            if (swipe.swipeUp)
            {
                verticalVelocity = jumpForce;
                anim.SetBool("isJumping", true);
                if (isSliding)
                    StopSliding();
            }
            else if (swipe.swipeDown && !isSliding)
            {
                anim.SetBool("isSliding", true);
                isSliding = true;
                controller.height = 0.1f;
                controller.center = new Vector3(controller.center.x, 0f, controller.center.z);
            }
            else if (nextJump)
            {
                nextJump = false;
                verticalVelocity = jumpForce;
                anim.SetBool("isJumping", true);
                if (isSliding)
                    StopSliding();
            }
        }
        else
        {
            verticalVelocity -= (gravity * Time.deltaTime);

            if (swipe.swipeDown)
            {
                verticalVelocity = -jumpForce;
                anim.SetBool("isSliding", true);
                isSliding = true;
                controller.height = 0.1f;
                controller.center = new Vector3(controller.center.x, 0f, controller.center.z);
            }
        }

        moveVector.y = verticalVelocity;
        moveVector.z = runSpeed;

        //Move Player
        controller.Move(moveVector * Time.deltaTime);

        //Rotate Player
        Vector3 dir = controller.velocity;
        if (dir != Vector3.zero)
        {
            dir.y = 0;
            transform.forward = Vector3.Lerp(transform.forward, dir, 0.1f);
        }
        
        if(moveVector.x < 8 && moveVector.x > -8 && isGrounded)
        {
            if (currentLane == 0 && transform.position.x != 0)
            {
                transform.position = new Vector3(0, transform.position.y, transform.position.z);
            }
            else if (currentLane == -1 && transform.position.x != -1)
            {
                transform.position = new Vector3(-1, transform.position.y, transform.position.z);
            }
            else if (currentLane == 1 && transform.position.x != 1)
            {
                transform.position = new Vector3(1, transform.position.y, transform.position.z);
            }
        }        
    }

    private void MoveLane(bool goingRight)
    {
        currentLane += (goingRight) ? 1 : -1;
        currentLane = Mathf.Clamp(currentLane, -1, 1);
    }

    public void StartRunning()
    {
        anim.SetBool("isWalking", false);
        anim.SetBool("isRunning", true);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.gameObject.tag == "Obstacle")
        {
            verticalVelocity = -jumpForce;
            isGameStarted = false;
            gameOver = true;
            resumeSpeed = runSpeed;
            runSpeed = 0;
            gameManager.pressedStarter = false;
            anim.SetBool("isAlive", false);
        }

        if (hit.gameObject.tag == "Road")
            isGrounded = true;
    }

    public void StopSliding()
    {
        anim.SetBool("isSliding", false);
        isSliding = false;
        controller.height = 0.89f;
        controller.center = new Vector3(controller.center.x, 0.5f, controller.center.z);
    }

    public void ResetGame(bool playAgain)
    {
        runSpeed = originalSpeed;
        modifier = runSpeed;
        anim.SetBool("isAlive", true);
        gameOver = false;
        Debug.Log(transform.position.z);
        currentLane = -1;
        if (playAgain)
        {
            if (currentLane == -1)
                MoveLane(true);
            else if (currentLane == 1)
                MoveLane(false);
        }            
        else
        {
            for (int i = 0; i <= currentLane; i++)
            {
                MoveLane(false);
            }

            menuElements.SetActive(true);
        }

        transform.position = new Vector3(-1, 0.1f, -8);
        Debug.Log(transform.position.z);
        transform.eulerAngles = new Vector3(transform.rotation.eulerAngles.x, transform.rotation.y - 90, transform.rotation.eulerAngles.z);


    }

    public void SetSkin(int id)
    {
        cache.SetSkinID(id);
        PlayerPrefs.Save();
        for (int i = 0; i < skins.Count; i++)
        {
            if (id == i)
                skins[i].SetActive(true);
            else
                skins[i].SetActive(false);
        }
    }
}
