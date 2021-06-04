using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoombaController : MonoBehaviour
{
    private int moveRight = 1;
    private Vector2 velocity;
    private float speed = 4.0f;

    private Rigidbody2D enemyBody;

    private float originalX;
    private float originalY;

    private bool stopMovement = false;
    private bool goombaIsDead = false;
    private Animator goombaAnimator;

    private bool marioIsDead = false;

    // Start is called before the first frame update
    void Start()
    {
        enemyBody = GetComponent<Rigidbody2D>();
        originalX = transform.position.x;
        originalY = transform.position.y;
        ComputeVelocity();
        goombaAnimator = GetComponent<Animator>();

    }

    void ComputeVelocity()
    {
        velocity = new Vector2(speed * moveRight, 0); //new Vector2((moveRight) * maxOffset / enemyPatroltime, 0);
    }

    void MoveGomba()
    {
        enemyBody.MovePosition(enemyBody.position + velocity * Time.fixedDeltaTime);
    }

    public void ResetGombaPosition()
    {
        transform.position = new Vector2(originalX, originalY);
        stopMovement = false;
        marioIsDead = false;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (marioIsDead || goombaIsDead)
        {
            return;
        }

        if (other.gameObject.CompareTag("Player"))
        {
            // ------------- CODE TO CHECK IF PLAYER COLLIDE WITH TOP OF GOOMBA -------------
            foreach (ContactPoint2D hitPos in other.contacts)
            {
                Vector2 hitPoint = hitPos.point;
                if (hitPoint.y > 0.15f && !goombaIsDead)
                {
                    stopMovement = true;
                    FindObjectOfType<AudioManager>().killGoomba();
                    FindObjectOfType<PlayerController>().flattenGoomba();
                    goombaAnimator.SetBool("isFlattened", true);
                    goombaIsDead = true;

                    Destroy(gameObject, 1);
                    return;
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
            // ------------- CODE TO CHECK PLAYER AND GOOMBA COLLISION END -------------

        }

        else if(!other.gameObject.CompareTag("FlyingAxe"))
        {
            moveRight *= -1;
            ComputeVelocity();
        }
    }

    public void MarioDead()
    {
        marioIsDead = true;
        stopMovement = true;
    }

    public void StopGoombaMovement()
    {
        stopMovement = true;
    }

    public void DestroyGoomba()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (!stopMovement)
        {
            MoveGomba();
        }
        
    }
}
