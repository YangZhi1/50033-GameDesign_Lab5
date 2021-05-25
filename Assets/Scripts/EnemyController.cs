using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private float maxOffset = 7.5f;
    private float enemyPatroltime = 2.0f;
    private int moveRight = -1;
    private Vector2 velocity;

    private Rigidbody2D enemyBody;

    private float originalX;
    private float originalY;

    public static bool stopMovement = false;
    public static bool restartedGame = false;


    // Start is called before the first frame update
    void Start()
    {
        enemyBody = GetComponent<Rigidbody2D>();
        originalX = transform.position.x;
        originalY = transform.position.y;
        ComputeVelocity();
    }

    void ComputeVelocity()
    {
        velocity = new Vector2((moveRight) * maxOffset / enemyPatroltime, 0);
    }

    void MoveGomba()
    {
        enemyBody.MovePosition(enemyBody.position + velocity * Time.fixedDeltaTime);
    }

    void ResetGombaPosition()
    {
        transform.position = new Vector2(originalX, originalY);
        restartedGame = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (restartedGame)
        {
            ResetGombaPosition();
        }

        if (!stopMovement)
        {
            if (Mathf.Abs(enemyBody.position.x - originalX) < maxOffset)
            {
                // move gomba
                MoveGomba();
            }

            else
            {
                // change direction
                moveRight *= -1;
                ComputeVelocity();
                MoveGomba();
            }
        }
        
    }
}
