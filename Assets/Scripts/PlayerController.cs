using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    // original position
    private float originalX;
    private float originalY;

    // boolean value to reset mario's position
    public static bool restartedGame = false;

    // speed values
    public static float speed = 40;
    private Rigidbody2D marioBody;
    public static float maxSpeed = 40;
    private bool onGroundState = true;
    public static float upSpeed = 30;

    // mario's sprite to face left and right
    private SpriteRenderer marioSprite;
    private bool faceRightState = true;

    public Transform enemyLocation;

    // score 
    public Text scoreText;
    public static int score = 0;
    private bool countScoreState = false;

    // audio 
    [SerializeField] private AudioSource jumpSound;
    [SerializeField] private AudioSource marioDie;
    private bool dieSoundPlayed = false;

    // stuff for mario dying "animation"
    private BoxCollider2D boxCollider2d;
    private bool intoTheAbyss = false;
    private bool fallingDownNow = false;
    private float maxHeight;


    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 30;
        marioBody = GetComponent<Rigidbody2D>();
        marioSprite = GetComponent<SpriteRenderer>();
        boxCollider2d = GetComponent<BoxCollider2D>();

        originalX = transform.position.x;
        originalY = transform.position.y;
        maxHeight = originalY + 3.0f;
    }

    // Update is called once per frame
    void Update()
    {
        // toggle state
        if ((Input.GetKeyDown("a") || Input.GetKeyDown("left")) && faceRightState)
        {
            faceRightState = false;
            marioSprite.flipX = true;
        }

        if ((Input.GetKeyDown("d") || Input.GetKeyDown("right")) && !faceRightState)
        {
            faceRightState = true;
            marioSprite.flipX = false;
        }

        if (!onGroundState && countScoreState)
        {
            if(Mathf.Abs(transform.position.x - enemyLocation.position.x) < 0.5f)
            {
                countScoreState = false;
                score++;
                Debug.Log(score);
            }
        }

        scoreText.text = "Score: " + score.ToString();
    }

    private void FixedUpdate()
    {
        // for mario dying animation
        if (intoTheAbyss)
        {
            // code to move mario body up
            if (!fallingDownNow)
            {
                Vector2 goingUp = new Vector2(0, 0.2f);
                marioBody.MovePosition(marioBody.position + goingUp);
            }

            if(transform.position.y > maxHeight)
            {
                fallingDownNow = true;
            }
        }

        if (restartedGame)
        {
            ResetMarioPosition();
        }
        // dynamic rigidbody
        float moveHorizontal = Input.GetAxis("Horizontal");
        if (Mathf.Abs(moveHorizontal) > 0)
        {
            Vector2 movement = new Vector2(moveHorizontal, 0);
            if (marioBody.velocity.magnitude < maxSpeed)
                marioBody.AddForce(movement * speed);
        }


        if (Input.GetKeyDown("space") && onGroundState)
        {
            jumpSound.Play();
            marioBody.AddForce(Vector2.up * upSpeed, ForceMode2D.Impulse);
            onGroundState = false;
            countScoreState = true; // check if Gomba is underneath
        }

        if (Input.GetKeyUp("a") || Input.GetKeyUp("d"))
        {
            // stop
            marioBody.velocity = Vector2.zero;
        }

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            onGroundState = true;
            countScoreState = false; // reset score state
            scoreText.text = "Score: " + score.ToString();
        }
    }

    void ResetMarioPosition()
    {
        transform.position = new Vector2(originalX, originalY);
        restartedGame = false;
        boxCollider2d.isTrigger = false;
        intoTheAbyss = false;
        fallingDownNow = false;
        dieSoundPlayed = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            // code to only play the die sound once
            if (!dieSoundPlayed)
            {
                MenuController.isDead = true;
                marioDie.Play();
                dieSoundPlayed = true;
            }
            FindObjectOfType<GameManager>().EndGame();

            // code to make mario fall into the abyss
            boxCollider2d.isTrigger = true;
            intoTheAbyss = true;
        }
    }

}
