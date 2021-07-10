using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushController : MonoBehaviour
{
    private Vector2 direction;
    private Rigidbody2D mushroom;
    private Vector2 velocity;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("mushroom created");
        System.Random rnd = new System.Random();
        int startDirection = rnd.Next(0, 2);
        if (startDirection == 0)
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
            StartCoroutine(enlarge());
            //velocity = new Vector2(0, 0);
        }
    }

    IEnumerator enlarge()
    {
        int steps = 5;
        float stepper = 0.5f / (float)steps;

        for (int i = 0; i < steps; i++)
        {
            this.transform.localScale = new Vector3(this.transform.localScale.x + stepper, this.transform.localScale.y + stepper, this.transform.localScale.z);
            yield return null;
        }
        Destroy(gameObject);
        this.gameObject.SetActive(false);
        yield break;
    }

    public void DestroyMushroom()
    {
        Destroy(this.gameObject);
    }
}
