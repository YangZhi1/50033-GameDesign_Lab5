using System.Collections;
using UnityEngine;
using UnityEngine.Events;


public class PlayerControllerEV : MonoBehaviour
{
    private float force;
    public IntVariable marioUpSpeed;
    public IntVariable marioMaxSpeed;
    public GameConstants gameConstants;

    private bool isDead = false;
    private bool faceRightState = true;


    private bool onGroundState = true;
    private bool isSpacebarUp = true;

    private bool isADKeyUp = true;

    private Rigidbody2D marioBody;

    private Animator marioAnimator;
    private bool countScoreState;

    private SpriteRenderer marioSprite;

    public UnityEvent onPlayerDeath;
    public CustomCastEvent castPowerup;

    public AudioSource marioDeadSound;

    // other components and internal state
    private void Start()
    {
        marioUpSpeed.SetValue(gameConstants.playerMaxJumpSpeed);
        marioMaxSpeed.SetValue(gameConstants.playerMaxSpeed);
        Debug.Log("Mario up speed: " + marioUpSpeed.Value);
        Debug.Log("Mario max speed: " + marioMaxSpeed.Value);

        force = gameConstants.playerDefaultForce;

        marioBody = GetComponent<Rigidbody2D>();
        marioSprite = GetComponent<SpriteRenderer>();

        marioAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown("a") || Input.GetKeyDown("left"))
        {
            isADKeyUp = false;
            if (faceRightState)
            {
                if (Mathf.Abs(marioBody.velocity.x) > 1.0)
                {
                    marioAnimator.SetTrigger("onSkid");
                }
                faceRightState = false;
                marioSprite.flipX = true;
            }
        }

        if ((Input.GetKeyDown("d") || Input.GetKeyDown("right")))
        {
            isADKeyUp = false;
            if (!faceRightState)
            {
                if (Mathf.Abs(marioBody.velocity.x) > 1.0)
                {
                    marioAnimator.SetTrigger("onSkid");
                }
                faceRightState = true;
                marioSprite.flipX = false;
            }
        }

        if (Input.GetKeyUp("a") || Input.GetKeyUp("d") || Input.GetKeyUp("left") || Input.GetKeyUp("right"))
        {
            isADKeyUp = true;
        }

        if (Input.GetKeyDown("down") || Input.GetKeyDown("s"))
        {
            marioAnimator.SetBool("downKeyPressed", true);
        }

        if (Input.GetKeyDown("space"))
        {
            isSpacebarUp = false;
        }

        if (Input.GetKeyUp("space"))
        {
            isSpacebarUp = true;
        }

        updateCollider();

        // for consumption of power ups
        if (Input.GetKeyDown("z"))
        {
            castPowerup.Invoke(KeyCode.Z);
            //CentralManager.centralManagerInstance.consumePowerup(KeyCode.Z, this.gameObject);

        }

        if (Input.GetKeyDown("x"))
        {
            castPowerup.Invoke(KeyCode.X);
            //CentralManager.centralManagerInstance.consumePowerup(KeyCode.X, this.gameObject);
        }

        marioAnimator.SetBool("onGround", onGroundState);
        marioAnimator.SetFloat("xSpeed", Mathf.Abs(marioBody.velocity.x));
        //coinCountText.text = "x " + coinCount.ToString();
    }

    private void FixedUpdate()
    {
        if (!isDead)
        {
            //check if a or d is pressed currently
            if (!isADKeyUp)
            {
                float direction = faceRightState ? 1.0f : -1.0f;
                Vector2 movement = new Vector2(force * direction, 0);
                if (marioBody.velocity.magnitude < marioMaxSpeed.Value)
                    marioBody.AddForce(movement);
            }

            if (!isSpacebarUp && onGroundState)
            {
                marioBody.AddForce(Vector2.up * marioUpSpeed.Value, ForceMode2D.Impulse);
                onGroundState = false;
                // part 2
                marioAnimator.SetBool("onGround", onGroundState);
                countScoreState = true; //check if Gomba is underneath
            }

        }
        
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground") || col.gameObject.CompareTag("Obstacles") || col.gameObject.CompareTag("Pipe") || col.gameObject.CompareTag("MoneyBrick"))
        {
            onGroundState = true;
        }
    }

    private void updateCollider()
    {
        Vector2 S = gameObject.GetComponent<SpriteRenderer>().sprite.bounds.size;
        gameObject.GetComponent<BoxCollider2D>().size = S;
        //gameObject.GetComponent<BoxCollider2D>().offset = new Vector2((S.x / 2), 0);
    }

    public void PlayerDiesSequence()
    {
        isDead = true;
        FindObjectOfType<GameManagerEV>().playerDead();
        //marioAnimator.SetBool("isDead", true);
        GetComponent<Collider2D>().enabled = false;
        marioBody.AddForce(Vector3.up * 30, ForceMode2D.Impulse);
        marioBody.gravityScale = 10;
        marioDeadSound.Play();
        StartCoroutine(dead());
    }

    IEnumerator dead()
    {
        yield return new WaitForSeconds(1.0f);
        marioBody.bodyType = RigidbodyType2D.Static;
    }

    public void flattenGoomba()
    {
        Debug.Log("Hit Goomba!");
    }
}