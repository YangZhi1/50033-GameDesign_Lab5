using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoombaController : MonoBehaviour
{
    public GameConstants gameConstants;

    private int moveRight = 1;
    private Vector2 velocity;
    private float speed;

    private Rigidbody2D enemyBody;

    private float originalX;
    private float originalY;

    private bool stopMovement = false;
    private bool goombaIsDead = false;
    private Animator goombaAnimator;

    private bool playerIsDead = false;
    private float enemyDanceFrequency;

    // Start is called before the first frame update
    void Start()
    {
        speed = gameConstants.enemySpeed;

        enemyBody = GetComponent<Rigidbody2D>();
        originalX = transform.position.x;
        originalY = transform.position.y;
        ComputeVelocity();
        goombaAnimator = GetComponent<Animator>();

        GameManager.OnPlayerDeath += EnemyRejoice;

        enemyDanceFrequency = gameConstants.enemyDanceFrequency;

        stopMovement = false;
        goombaIsDead = false;
    }

    void ComputeVelocity()
    {
        velocity = new Vector2(speed * moveRight, 0); //new Vector2((moveRight) * maxOffset / enemyPatroltime, 0);
    }

    void MoveGomba()
    {
        enemyBody.MovePosition(enemyBody.position + velocity * Time.fixedDeltaTime);
    }

    public void ResetEnemy()
    {
        stopMovement = false;
        playerIsDead = false;
        goombaIsDead = false;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (goombaIsDead)
        {
            return;
        }

        if (other.gameObject.CompareTag("Player"))
        {
            ContactPoint2D contact = other.GetContact(0);
            float yContact = contact.point.y;

            float enemyTopY = this.transform.position.y + GetComponent<SpriteRenderer>().bounds.extents.y / 2;
            
            if(yContact > enemyTopY)
            {
                // maybe don't set stop movement to true? 
                stopMovement = true;
                FindObjectOfType<AudioManager>().killGoomba();
                FindObjectOfType<PlayerController>().flattenGoomba();
                goombaAnimator.SetBool("isFlattened", true);
                goombaIsDead = true;
                StartCoroutine(flatten());
            }
            else
            {
                if (goombaIsDead)
                {
                    return;
                }
                else
                {
                    FindObjectOfType<PlayerController>().MarioHitByGoomba();
                }
            }
        }

        else if(!other.gameObject.CompareTag("FlyingAxe") && !other.gameObject.CompareTag("Ground"))
        {
            transform.Rotate(new Vector3(0, 180.0f, 0));
            moveRight *= -1;
            ComputeVelocity();
        }
    }

    public void StopGoombaMovement()
    {
        stopMovement = true;
    }

    public void DestroyGoomba()
    {
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!stopMovement)
        {
            MoveGomba();
        }

        if (goombaIsDead)
        {
            updateCollider();
        }

        if (playerIsDead)
        {
            if(enemyDanceFrequency > 0)
            {
                enemyDanceFrequency -= Time.deltaTime;
            }
            else
            {
                this.transform.Rotate(0, 180.0f, 0);
                enemyDanceFrequency = gameConstants.enemyDanceFrequency;
            }
        }
        
    }
    IEnumerator flatten()
    {
        int steps = 5;
        float stepper = 1.0f / (float)steps;

        /*for (int i = 0; i < steps; i++)
        {
            this.transform.localScale = new Vector3(this.transform.localScale.x, this.transform.localScale.y - stepper, this.transform.localScale.z);

            // make sure enemy is still above ground
            //this.transform.position = new Vector3(this.transform.position.x, gameConstants.groundSurface + GetComponent<SpriteRenderer>().bounds.extents.y, this.transform.position.z);
            yield return null;
        }*/

        yield return new WaitForSeconds(1);
        this.gameObject.SetActive(false);

        // spawn 2 new enemies
        if (this.gameObject.name == "gomba1(Clone)")
        {
            SpawnManager.SpawnManagerInstance.spawnOnDeath(ObjectType.gombaEnemy, this.transform.position.x);
        }
        else
        {
            SpawnManager.SpawnManagerInstance.spawnOnDeath(ObjectType.greenEnemy, this.transform.position.x);
        }


        yield break;
    }

    private void updateCollider()
    {
        try
        {
            Vector2 S = gameObject.GetComponent<SpriteRenderer>().sprite.bounds.size;
            gameObject.GetComponent<BoxCollider2D>().size = S;
        }
        catch { }
    }


    // animation when player is dead
    void EnemyRejoice()
    {
        Debug.Log("Enemy killed Mario");
        stopMovement = true;
        playerIsDead = true;
        // do whatever you want here, animate etc
        // ...
    }

}
