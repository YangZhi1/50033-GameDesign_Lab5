using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    // original position
    private float originalX;
    private float originalY;

    // speed values
    public float speed = 40;
    private Rigidbody2D marioBody;
    public float maxSpeed = 40;
    private bool onGroundState = true;
    public float upSpeed = 30;

    //-------------------------- ANIMATION STUFF --------------------------
    private Animator marioAnimator;
    private bool marioIsBig = false; // boolean to determine if is small mario or big mario
    private bool marioInvincible = false; // for when mario is invincible
    private float marioInvincibleDuration;
    [SerializeField] Sprite smallMarioIdle;
    [SerializeField] Sprite bigMarioIdle;
    // mario's sprite to face left and right
    private SpriteRenderer marioSprite;
    private bool faceRightState = true;

    private bool dieSoundPlayed = false;

    // stuff for mario dying "animation"
    private BoxCollider2D boxCollider2d;
    private bool intoTheAbyss = false;
    private bool fallingDownNow = false;
    private float maxHeight;

    // coin count
    [SerializeField] private Text coinCountText;
    private int coinCount = 0;

    // throwing axe
    [SerializeField] private GameObject throwingAxe;

    // Pipe that can be entered
    [SerializeField] private GameObject pipeTeleport1;

    [SerializeField] private ParticleSystem dustCloud;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 30;
        marioBody = GetComponent<Rigidbody2D>();
        marioSprite = GetComponent<SpriteRenderer>();
        boxCollider2d = GetComponent<BoxCollider2D>();

        originalX = transform.position.x;
        originalY = transform.position.y;

        marioAnimator = GetComponent<Animator>();

        updateCollider();

        GameManager.OnPlayerDeath += MarioDead;
    }

    private void updateCollider()
    {
        Vector2 S = gameObject.GetComponent<SpriteRenderer>().sprite.bounds.size;
        gameObject.GetComponent<BoxCollider2D>().size = S;
        //gameObject.GetComponent<BoxCollider2D>().offset = new Vector2((S.x / 2), 0);
    }

    // Update is called once per frame
    void Update()
    {
        // toggle state
        if ((Input.GetKeyDown("a") || Input.GetKeyDown("left")) && faceRightState)
        {
            if (Mathf.Abs(marioBody.velocity.x) > 1.0)
            {
                marioAnimator.SetTrigger("onSkid");
            }
            faceRightState = false;
            marioSprite.flipX = true;
        }

        if ((Input.GetKeyDown("d") || Input.GetKeyDown("right")) && !faceRightState)
        {
            if (Mathf.Abs(marioBody.velocity.x) > 1.0)
            {
                marioAnimator.SetTrigger("onSkid");
            }
            faceRightState = true;
            marioSprite.flipX = false;
        }

        if (Input.GetKeyDown("t"))
        {
            marioThrowAxe();
        }

        if (Input.GetKeyDown("down") || Input.GetKeyDown("s"))
        {
            marioAnimator.SetBool("downKeyPressed", true);
        }

        if(Mathf.Abs(transform.position.x - pipeTeleport1.transform.position.x) < 2.0f)
        {
            FindObjectOfType<MenuController>().nothingToSeeHere();
        }

        if (Mathf.Abs(transform.position.x - pipeTeleport1.transform.position.x) > 2.0f)
        {
            FindObjectOfType<MenuController>().hideNothingToSeeHere();
        }

        if (marioInvincible)
        {
            if(marioInvincibleDuration > 0)
            {
                marioInvincibleDuration -= Time.deltaTime;
            }
            else
            {
                marioInvincible = false;
            }
        }

        // for consumption of power ups
        if (Input.GetKeyDown("z"))
        {
            CentralManager.centralManagerInstance.consumePowerup(KeyCode.Z, this.gameObject);
        }

        if (Input.GetKeyDown("x"))
        {
            CentralManager.centralManagerInstance.consumePowerup(KeyCode.X, this.gameObject);
        }

        marioAnimator.SetBool("onGround", onGroundState);
        marioAnimator.SetFloat("xSpeed", Mathf.Abs(marioBody.velocity.x));
        coinCountText.text = "x " + coinCount.ToString();
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
            PlayJumpSound(); // done in animator
            marioBody.AddForce(Vector2.up * upSpeed, ForceMode2D.Impulse);
            onGroundState = false;
        }

        if (Input.GetKeyUp("a") || Input.GetKeyUp("d"))
        {
            // stop
            marioBody.velocity = Vector2.zero;
        }

        if (Input.GetKeyUp("down") || Input.GetKeyUp("s"))
        {
            marioAnimator.SetBool("downKeyPressed", false);
        }

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground") || col.gameObject.CompareTag("Obstacles") || col.gameObject.CompareTag("Pipe"))
        {
            onGroundState = true;
            dustCloud.Play();
        }

        if (col.gameObject.CompareTag("PipeTeleport"))
        {
            onGroundState = true;
            dustCloud.Play();
        }

        if (col.gameObject.CompareTag("Spawned"))
        {
            if (marioIsBig)
            {
                FindObjectOfType<AudioManager>().collectCoin(); // using coin sound for mushroom lol
                CentralManager.centralManagerInstance.increaseScore(500);
            }
            else
            {
                FindObjectOfType<AudioManager>().mushroomBecomeBig();
                marioSprite.sprite = bigMarioIdle;
                updateCollider();
                marioIsBig = true;
                marioAnimator.SetBool("isBig", marioIsBig);
            }
            
        }
    }

    public void MarioHitByGoomba()
    {
        if (marioIsBig)
        {
            // currently separating the two because I might implement invincible mushroom
            if (!marioInvincible)
            {
                marioSprite.sprite = smallMarioIdle;
                updateCollider();
                FindObjectOfType<AudioManager>().marioFall();
                marioIsBig = false;
                marioAnimator.SetBool("isBig", marioIsBig);
                marioInvincible = true;
                marioInvincibleDuration = 2.0f;
            }
        }

        else
        {
            if (!marioInvincible)
            {
                CentralManager.centralManagerInstance.damagePlayer();
                // MarioDead();
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PipeTeleport"))
        {
            if (Input.GetKeyDown("down") || Input.GetKeyDown("s"))
            {
                FindObjectOfType<CameraController>().PlayerWarpedToToilet();
                FindObjectOfType<AudioManager>().marioWarped();
                transform.position = new Vector2(-2.2f, -10.5f);
            }
        }
    }


    public void RestartGame()
    {
        // make mario small again
        marioSprite.sprite = smallMarioIdle;
        updateCollider();
        marioIsBig = false;
        marioAnimator.SetBool("isBig", marioIsBig);

        // reset mario's position
        transform.position = new Vector2(originalX, originalY);
        
        // booleans for audio and "animation" of mario's death
        boxCollider2d.enabled = true;
        intoTheAbyss = false;
        fallingDownNow = false;
        dieSoundPlayed = false;

        // reset speed to be able to move
        speed = 40;
        maxSpeed = 40;
        upSpeed = 30;
        coinCount = 0;
        CentralManager.centralManagerInstance.resetScore();
    }

    public void MarioDead()
    {
        // code to only play the die sound once
        if (!dieSoundPlayed)
        {
            FindObjectOfType<MenuController>().stopSong();
            FindObjectOfType<AudioManager>().marioDead();
            dieSoundPlayed = true;
        }

        // freeze mario's movements
        speed = 0;
        maxSpeed = 0;
        upSpeed = 0;

        FindObjectOfType<GameManager>().EndGame();

        // code to make mario fall into the abyss
        boxCollider2d.enabled = false;
        intoTheAbyss = true;
        maxHeight = transform.position.y + 3.0f;
    }

    public void WinGame()
    {
        speed = 0;
        maxSpeed = 0;
        upSpeed = 0;
    }

    public void StopMarioMovements()
    {
        // freeze mario's movements
        speed = 0;
        maxSpeed = 0;
        upSpeed = 0;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            // if mario haven't die yet
            if (!intoTheAbyss)
            {
                marioCollectCoin();
            }
        }

        if (other.gameObject.CompareTag("ToiletPipe"))
        {
            transform.position = new Vector2(28.6f, 4.0f);
            FindObjectOfType<AudioManager>().marioWarped();
            FindObjectOfType<CameraController>().PlayerWarpedBack();
        }
    }
    
    public void flattenGoomba()
    {
        CentralManager.centralManagerInstance.increaseScore(100);
        marioBody.AddForce(Vector2.up * 15, ForceMode2D.Impulse);
    }

    void PlayJumpSound()
    {
        if (marioIsBig)
        {
            FindObjectOfType<AudioManager>().marioBigJump();
        }
        else
        {
            FindObjectOfType<AudioManager>().marioSmallJump();
        }
    }

    public void marioCollectCoin()
    {
        FindObjectOfType<AudioManager>().collectCoin();
        coinCount++;
    }

    void marioThrowAxe()
    {
        float xDisplacement;
        float xSpeed = 15.0f;

        if (faceRightState)
        {
            xDisplacement = this.transform.position.x + 0.8f; 
        }
        else
        {
            xDisplacement = this.transform.position.x - 0.8f;
            xSpeed *= -1;
        }

        GameObject newAxe = Instantiate(throwingAxe, new Vector3(xDisplacement, this.transform.position.y, this.transform.position.z), Quaternion.identity);
        FindObjectOfType<ThrowingAxe>().setDirection(faceRightState);
        newAxe.GetComponent<Rigidbody2D>().velocity = new Vector2(xSpeed, 6.0f);
    }
}
