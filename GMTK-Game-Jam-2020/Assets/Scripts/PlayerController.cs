using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    private InputManager inputs;
    private Animator animator;
    private Rigidbody2D rigidbody;

    public int maxSpeed = 1;
    public int acceleration = 1;
    public int traction = 70;
    public float jumpPower;
    public int jumpFrameCount;
    public Vector2 velocity;
    public float currentSpeed;
    public int gravity;

    public bool inAir;
    public bool onGround;
    public bool jumping;
    public bool ascending;
    public int jumpCount;

    // The reducers are used to use float values because we're moving in such a small space that 
    // writing small floats like 0.001 for public values would be ugly
    float maxSpeedReducer;
    float accelerationReducer;
    float tractionReducer;
    float jumpReducer;
    float gravityReducer;
    float turningTraction = 0.10f;
    float minVelocity = 0;

    private Coroutine jumpCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        inputs = GameObject.Find("GameManager").GetComponent<InputManager>();
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        velocity = new Vector2(0, 0);
        maxSpeedReducer = maxSpeed * 0.1f;
        accelerationReducer = acceleration * 0.001f; // We reduce the velocity and traction by this multiplier to keep the public numbers simple
        tractionReducer = traction * 0.001f;
        jumpReducer = jumpPower * 0.1f;
        gravityReducer = gravity * 0.01f;
    }

    void Update()
    {
        HandleControls();
        HandleReset();
    }

    private void FixedUpdate()
    {
        HandleVelocity();
        HandleGravity();
        currentSpeed = velocity.magnitude;
    }

    private void HandleReset()
    {
        if (Input.GetKeyDown(inputs.reset))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void HandleGravity()
    {
        if (onGround || ascending) return;

        if (velocity.y > minVelocity)
        {
            velocity.y -= gravityReducer;
        }
        if (velocity.y < minVelocity)
        {
            velocity.y = minVelocity;
        }
    }

    private void HandleVelocity()
    {
        float x = transform.position.x;
        float y = transform.position.y;
        if (velocity.x != 0)
        {
            animator.SetBool("Running", true);
        }

        float newXVelocity = x + velocity.x * Time.deltaTime;
        float newYVelocity = onGround && !jumping ? y : y + velocity.y * Time.deltaTime;
        transform.position = new Vector2(newXVelocity, newYVelocity);
    }

    private void HandleControls()
    {
        HandleMovementControls();
        HandleJumpControls();
    }

    private void HandleMovementControls()
    {
        bool hasMovement = false;
        float xVelocity = velocity.x;
        if (Input.GetKey(inputs.leftMove) && xVelocity > -maxSpeed)
        {
            if (xVelocity > 0)
            {
                xVelocity -= xVelocity * turningTraction;
            }
            hasMovement = true;
            float newXVelocity = xVelocity - accelerationReducer;
            if (newXVelocity < -maxSpeedReducer)
            {
                newXVelocity = -maxSpeedReducer;
            }

            velocity = new Vector2(newXVelocity, velocity.y);
            transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        }
        if (Input.GetKey(inputs.rightMove) && xVelocity < maxSpeed && !hasMovement)
        {
            if (xVelocity < 0)
            {
                xVelocity -= xVelocity * turningTraction;
            }
            hasMovement = true;
            float newXVelocity = xVelocity + accelerationReducer;
            if (newXVelocity > maxSpeedReducer)
            {
                newXVelocity = maxSpeedReducer;
            }

            velocity = new Vector2(newXVelocity, velocity.y);
            transform.rotation = Quaternion.Euler(Vector3.zero);
        }

        if (!hasMovement)
        {
            animator.SetBool("Running", false);
            SlowDownVelocity();
        }
    }

    private void HandleJumpControls()
    {
        if (Input.GetKeyDown(inputs.jump) && jumpCount < 2)
        {
            jumpCount++;
            if (!onGround && jumpCount == 0)
            {
                jumpCount++;
            }
            if (jumpCoroutine != null)
            {
                StopCoroutine(jumpCoroutine);
            }

            jumpCoroutine = StartCoroutine(HandleJump());
        }
    }

    private IEnumerator HandleJump()
    {
        jumping = true;
        ascending = true;
        bool isDoubleJump = jumpCount >= 2;
        if (isDoubleJump)
        {
            rigidbody.velocity = Vector3.zero;
        }

        float increment = jumpPower / jumpFrameCount;
        for (int i = 0; i < jumpFrameCount; i++)
        {
            velocity.y = jumpPower - (i * increment);
            yield return new WaitForEndOfFrame();
        }
        
        jumping = false;
        ascending = false;
        jumpCoroutine = null;
    }

    private void SlowDownVelocity()
    {
        float xVelocity = velocity.x - velocity.x * tractionReducer;
        float yVelocity = velocity.y;

        if (xVelocity < 0.001f && xVelocity > -0.001f)
        {
            xVelocity = 0;
            animator.SetBool("Running", false);
        }

        velocity = new Vector2(xVelocity, yVelocity);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        onGround = true;
        if (!jumping)
        {
            jumpCount = 0;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        onGround = false;
    }
}
