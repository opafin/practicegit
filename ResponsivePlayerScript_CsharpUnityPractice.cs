using System.Threading;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour
{

    [SerializeField] private InputActionReference move, jump, sprint, attack;
    [SerializeField] float runSpeed = 10f;
    [SerializeField] float sprintSpeed = 20f;
    [SerializeField] float climbSpeed = 10f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] CapsuleCollider2D myBodyCollider;
    [SerializeField] CapsuleCollider2D myFeetCollider;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform gun;
    Rigidbody2D myRigidbody;
    Animator myAnimator;
    Vector2 moveInput;
    float runSpeedAtStart;
    float gravityScaleAtStart;
    float timeNotGrabbing;
    float grabTimeBufferDuration = 1f;
    float grabTimeBuffer;
    float groundTimeBufferDuration = 0.025f;
    float groundTimeBuffer;
    float airTimeBufferDuration = 0.025f;
    float airTimeBuffer;
    float coyoteTime = 5f;
    float coyoteTimeCounter;
    bool doubleJump;
    bool isRunning;
    bool isGrabbing;
    bool isGrounded;
    bool isInTheAir;
    bool isAlive;



    void Start()
    {
        isAlive = true;
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        gravityScaleAtStart = myRigidbody.gravityScale;
        runSpeedAtStart = runSpeed;
    }

    void Update()
    {
        if (!isAlive)
            return;

        PlayerInput();
        StateManager();
    }

    void PlayerInput()
    {
        moveInput = move.action.ReadValue<Vector2>();

        switch (moveInput.y)
        {
            case > 0:
                OnUp();
                break;
            case
                < 0:
                OnDown();
                break;
            default:
                LevelGaze();
                break;
        }

        if (sprint.action.IsPressed())
        {
            runSpeed = sprintSpeed;
        }
        else
            runSpeed = runSpeedAtStart;
    }

    void StateManager()
    {
        IsRunning();
        IsGrabbing();
        IsGrounded();
        IsInTheAir();
        IsViewing();
        FlipSprite();
        // OnDeath();
    }

    void OnJump(InputValue value)
    {
        if (!isAlive) { return; }

        if (doubleJump)
        {
            myRigidbody.velocity = new Vector2(moveInput.x * runSpeed, jumpSpeed);
            coyoteTimeCounter = 0f;
            doubleJump = false;
            return;
        }

        if (isGrounded || isGrabbing || coyoteTimeCounter > 0)
        {
            Debug.Log("Can jump!");
            isGrabbing = false;
            myAnimator.SetBool("isGrabbing", false);
            myAnimator.SetBool("isClimbing", false);
            myRigidbody.gravityScale = gravityScaleAtStart;
            myRigidbody.velocity = new Vector2(moveInput.x * runSpeed, jumpSpeed);
            doubleJump = true;
        }
    }

    // void OnFire(InputValue value)
    // {
    //     if (!isAlive) { return; }
    //     Instantiate(bullet, gun.position, transform.rotation);
    // }

    void OnUp()
    {
        if (grabTimeBuffer <= 0f && !isGrabbing && myRigidbody.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            Grab();
        }

        if (isGrounded && !isGrabbing && !isRunning)
        {
            myAnimator.SetBool("isGazingUp", true);
        }
        else
            LevelGaze();
    }

    void OnDown()
    {
        if (isGrounded)
        {
            ExitLadder();

            if (!isRunning && timeNotGrabbing < -0.75f)
            {
                myAnimator.SetBool("isGazingDown", true);
            }
            else
                LevelGaze();
            return;
        }

        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")) && !isGrabbing && grabTimeBuffer <= 0f)
        {
            Grab();
        }

        if (isInTheAir)
            return;

        if (!isGrounded)
        {
            Vector2 climbingSnap = new Vector2(moveInput.x * 0, moveInput.y * climbSpeed);
            myRigidbody.velocity = climbingSnap;
        }
    }

    void IsRunning()
    {
        if (isGrabbing)
        {
            myAnimator.SetBool("isRunning", false);
            return;
        }

        myRigidbody.velocity = new Vector2(moveInput.x * runSpeed, myRigidbody.velocity.y);

        isRunning = (Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon);
        myAnimator.SetBool("isRunning", isRunning);
    }

    void IsGrounded()
    {
        if (isGrounded)
        {
            groundTimeBuffer = groundTimeBufferDuration;

        }
        else if (groundTimeBuffer <= 0 && myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            isGrounded = true;
            doubleJump = false;
        }

        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            isGrounded = false;
        }

        groundTimeBuffer -= Time.deltaTime;
    }

    void IsInTheAir()
    {

        if (isGrabbing || isGrounded)
        {
            isInTheAir = false;
        }

        if (isInTheAir)
        {
            airTimeBuffer = airTimeBufferDuration;
        }
        else if (airTimeBuffer <= 0 && !isGrabbing && !isGrounded)
        {
            coyoteTimeCounter = coyoteTime;
            isInTheAir = true;
        }

        if (!isGrabbing && !isGrounded)
            coyoteTimeCounter -= Time.deltaTime;

        airTimeBuffer -= Time.deltaTime;
    }

    void IsGrabbing()
    {
        if (!myRigidbody.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            isGrabbing = false;
            myAnimator.SetBool("isGrabbing", false);
            myAnimator.SetBool("isClimbing", false);
            myRigidbody.gravityScale = gravityScaleAtStart;
        }

        if (isGrabbing)
        {
            myAnimator.SetBool("isGrabbing", true);
            timeNotGrabbing = 0;

            if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("LadderEnds")) && (moveInput.y > 0))
                moveInput.y = 0;

            Vector2 climbingSnap = new Vector2(moveInput.x * 0, moveInput.y * climbSpeed);
            myRigidbody.velocity = climbingSnap;

            bool hasSpeed = (Mathf.Abs(myRigidbody.velocity.y) > 0);
            myAnimator.SetBool("isClimbing", hasSpeed);
        }

        if (!isGrabbing)
        {
            timeNotGrabbing -= Time.deltaTime;
            myAnimator.SetBool("isGrabbing", false);
            myAnimator.SetBool("isClimbing", false);
        }

        grabTimeBuffer -= Time.deltaTime;
    }

    void Grab()
    {
        myRigidbody.velocity = Vector2.zero;
        isGrabbing = true;
        grabTimeBuffer = grabTimeBufferDuration;
        myRigidbody.gravityScale = 0;
    }

    void ExitLadder()
    {
        isGrabbing = false;
        myRigidbody.gravityScale = gravityScaleAtStart;
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Ladder" && isGrabbing)
        {
            transform.localPosition = new Vector2(other.bounds.center.x, transform.localPosition.y);
        }
    }

    void IsViewing()
    {
        if (myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Viewing")) || myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Viewing")))
        {
            myAnimator.SetBool("isViewing", true);
        }
        else
        {
            myAnimator.SetBool("isViewing", false);
        }
    }

    void LevelGaze()
    {
        myAnimator.SetBool("isGazingDown", false);
        myAnimator.SetBool("isGazingUp", false);
    }
    void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > 0;

        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody.velocity.x), 1f);
        }
    }

    // void OnDeath()
    // {
    //     if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemies")))
    //     {
    //         isAlive = false;
    //         myAnimator.SetTrigger("isDead");
    //         Vector2 deathKick = new Vector2(20f, 20f);
    //         myRigidbody.velocity = deathKick;
    //         FindObjectOfType<Reaper>().Reap();
    //     }
    // }
}



