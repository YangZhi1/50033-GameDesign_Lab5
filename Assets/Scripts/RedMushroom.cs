using System.Collections;
using UnityEngine;

public class RedMushroom : MonoBehaviour, ConsumableInterface
{
    public Texture t;
    private int index = 0; // 0 is 'z' key, refer to power up manager

    private Vector2 direction;
    private Rigidbody2D mushroom;
    private Vector2 velocity;
    private SpriteRenderer sr;

    void Start()
    {
        GameManager.OnPlayerDeath += DestroyMushroom;
        GameManager.OnGameReset += DestroyMushroom;

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
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        mushroom.MovePosition(mushroom.position + velocity * Time.fixedDeltaTime * direction);
    }

    public void consumedBy(GameObject player)
    {
        // give player jump boost
        player.GetComponent<PlayerController>().upSpeed += 10;
        StartCoroutine(removeEffect(player));
    }

    IEnumerator removeEffect(GameObject player)
    {
        yield return new WaitForSeconds(5.0f);
        player.GetComponent<PlayerController>().upSpeed -= 10;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Pipe") || col.gameObject.CompareTag("Enemy") || col.gameObject.CompareTag("PipeTeleport") || col.gameObject.CompareTag("Spawned"))
        {
            direction *= new Vector2(-1, 0);
        }

        if (col.gameObject.CompareTag("Player"))
        {
            // update UI
            StartCoroutine(enlarge());
            CentralManager.centralManagerInstance.addPowerup(t, index, this);
            GetComponent<Collider2D>().enabled = false;
        }
    }

    public void onGameRestart()
    {
        Destroy(gameObject);
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
        //this.gameObject.SetActive(false);
        sr.enabled = false;
        yield break;
    }

    void DestroyMushroom()
    {
        Debug.Log("Destroy mushroom got problem");
        //Destroy(this.gameObject);
    }
}