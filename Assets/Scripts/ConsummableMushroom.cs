using System;
using System.Collections.Generic;
using UnityEngine;

public class ConsummableMushroom : MonoBehaviour
{
    private Vector2 direction;
    private Rigidbody2D mushroom;
    private Vector2 velocity;
    
    // Start is called before the first frame update
    void Start()
    {
        System.Random rnd = new System.Random();
        int startDirection = rnd.Next(0, 2);
        if(startDirection == 0)
        {
            direction = new Vector2(-1, 0);
        }
        else
        {
            direction = new Vector2(1, 0);
        }
        
        mushroom = GetComponent<Rigidbody2D>();
        mushroom.AddForce(Vector2.up * 20, ForceMode2D.Impulse);
        velocity = new Vector2(5, 0);
        
    }

    // Update is called once per frame
    void Update()
    {
        // code to move mushroom around
        //Vector2 currentPosition = consummablePrefab.transform.position;

        // Vector2 nextPosition = currentPosition + speed * currentDirection.Normalize() * Time.fixedDeltaTime;
        // rigidBody.MovePosition(nextPosition);

        mushroom.MovePosition(mushroom.position + velocity * Time.fixedDeltaTime * direction);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Pipe") || collision.gameObject.CompareTag("Enemy"))
        {
            direction *= new Vector2(-1, 0);
        }

        // if collide with player, stop moving
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            //velocity = new Vector2(0, 0);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    public void onGameRestart()
    {
        Destroy(gameObject);
    }
}
