using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    private InputManager inputs;
    private InputCalculator inputCalculator;
    private Animator animator;
    private Rigidbody2D rigidbody;
    private AudioSource playerAudioSource;
    private PersistentDataManager persistentDataManager;

    private ParticleSystem jumpParticles;
    private ParticleSystem rocketParticles;
    private ParticleSystem smokeParticles;

    public Text outOfControlsText;

    public AudioClip jumpSound;
    public AudioClip jetpackSound;
    public AudioClip deathSounds;
    public AudioClip victorySound1;
    public AudioClip victorySound2;

    public int maxSpeed = 1;
    public int acceleration = 1;
    public int traction = 70;
    public float jumpPower;
    public int jumpFrameCount;
    public Vector2 velocity;
    public float currentSpeed;
    public int gravity;
    public float boostMultiplier = 2.5f;

    private bool noMove = false;
    private bool inAir;
    private bool onGround;
    private bool jumping;
    private bool levelEnd;
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
        inputCalculator = GameObject.Find("GameManager").GetComponent<InputCalculator>();
        jumpParticles = GameObject.Find("JumpParticles").GetComponent<ParticleSystem>();
        rocketParticles = GameObject.Find("RocketParticles").GetComponent<ParticleSystem>();
        smokeParticles = GameObject.Find("SmokeParticles").GetComponent<ParticleSystem>();
        playerAudioSource = GameObject.Find("PlayerAudio").GetComponent<AudioSource>();
        if (GameObject.Find("PersistentDataManager"))
        {
            persistentDataManager = GameObject.Find("PersistentDataManager").GetComponent<PersistentDataManager>();
        }
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
        HandleReset();
        HandleExit();
        if (noMove) return;
        HandleControls();
    }

    private void FixedUpdate()
    {
        if (noMove) return;
        HandleVelocity();
        HandleGravity();
        currentSpeed = velocity.magnitude;
    }

    private void HandleOutOfInputs()
    {
        if (inputCalculator.movementControlsExhausted() && inputCalculator.jumpControlsExhausted() && !levelEnd)
        {
            outOfControlsText.enabled = true;
            TriggerDeath();
        }
    }

    private void HandleExit()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            persistentDataManager.GoToStage(0);
        }
    }

    private void HandleReset()
    {
        if (Input.GetKeyDown(inputs.reset))
        {
            DoReset();
        }
    }

    private void DoReset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void HandleGravity()
    {
        if (onGround || jumping) return;

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
        bool inputsHaveBeenExhausted = inputCalculator.movementControlsExhausted();

        if ((Input.GetKeyUp(inputs.leftMove) || Input.GetKeyUp(inputs.rightMove)) && !inputsHaveBeenExhausted)
        {
            inputCalculator.IncrementMovementCounter();
        }

        bool hasMovement = false;
        float xVelocity = velocity.x;
        if (Input.GetKey(inputs.leftMove) && xVelocity > -maxSpeed && !inputsHaveBeenExhausted)
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
        if (Input.GetKey(inputs.rightMove) && xVelocity < maxSpeed && !hasMovement && !inputsHaveBeenExhausted)
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
        if (inputCalculator.jumpControlsExhausted())
        {
            return;
        }

        if (Input.GetKeyDown(inputs.jump) && jumpCount < 2)
        {
            inputCalculator.IncrementJumpCounter();
            if (!onGround && jumpCount == 0)
            {
                jumpCount++;
            }
            jumpCount++;
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
        bool isDoubleJump = jumpCount >= 2;
        animator.SetTrigger("Jump");
        if (isDoubleJump)
        {
            playerAudioSource.clip = jetpackSound;
            playerAudioSource.Play();
            rocketParticles.Play();
            rigidbody.velocity = Vector3.zero;
        } else
        {
            playerAudioSource.clip = jumpSound;
            playerAudioSource.Play();
            jumpParticles.Play();
        }

        float increment = jumpPower / jumpFrameCount;
        for (int i = 0; i < jumpFrameCount; i++)
        {
            velocity.y = jumpPower - (i * increment);
            yield return new WaitForFixedUpdate();
        }
        
        jumping = false;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (jumpCount > 0 || animator.GetBool("InAir"))
        {
            animator.SetTrigger("Landing");
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        onGround = true;
        animator.SetBool("InAir", false);
        HandleOutOfInputs();
        if (!jumping)
        {
            jumpCount = 0;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        onGround = false;
        animator.SetBool("InAir", true);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && !noMove)
        {
            TriggerDeath();
        } 
    }

    private void TriggerDeath()
    {
        playerAudioSource.clip = deathSounds;
        playerAudioSource.Play();
        animator.SetBool("Death", true);
        noMove = true;
        StartCoroutine(WaitForReset());
    }

    public IEnumerator WaitForReset()
    {
        yield return new WaitForSeconds(1);
        smokeParticles.Play();
        yield return new WaitForSeconds(3);
        DoReset();
    }

    public void HandleLevelEnd()
    {
        StartCoroutine(HandleVictorySounds());
        levelEnd = true;
        noMove = true;
        animator.SetBool("Goal", true);
    }

    private IEnumerator HandleVictorySounds()
    {
        playerAudioSource.clip = victorySound1;
        playerAudioSource.Play();
        yield return new WaitForSeconds(0.4f);
        playerAudioSource.Play();
        yield return new WaitForSeconds(0.4f);
        playerAudioSource.Play();
        yield return new WaitForSeconds(0.8f);
        playerAudioSource.clip = victorySound2;
        playerAudioSource.Play();
    }

    public void HandleBoost()
    {
        StartCoroutine(Boost());
    }

    private IEnumerator Boost()
    {
        float oldMaxSpeed = maxSpeedReducer;
        maxSpeedReducer = oldMaxSpeed * boostMultiplier;
        velocity *= boostMultiplier;
        if (velocity.x > 40)
        {
            velocity = new Vector2(40,velocity.y);
        }
        yield return new WaitForSeconds(2.5f);
        maxSpeedReducer = oldMaxSpeed;

    }
}
