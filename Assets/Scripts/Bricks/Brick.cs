using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    private int breakCount = 5;
    [SerializeField] private GameObject breakableDownLeft;
    [SerializeField] private GameObject breakableDownRight;
    [SerializeField] private GameObject breakableUpLeft;
    [SerializeField] private GameObject breakableUpRight;
    [SerializeField] private GameObject disappearingCoin;

    private Vector2 brickSize;

    public AudioSource smb_coin;

    // Start is called before the first frame update
    void Start()
    {
        brickSize = gameObject.GetComponent<SpriteRenderer>().sprite.bounds.size;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            foreach (ContactPoint2D hitPos in collision.contacts)
            {
                Vector2 hitPoint = hitPos.point;
                if (hitPoint.y < transform.position.y - brickSize.y/4)
                {
                    breakCount--;
                    if (breakCount != 0)
                    {
                        // create coin above it
                        // up coin count
                        GameObject newCoin = Instantiate(disappearingCoin, new Vector3(this.transform.position.x, this.transform.position.y + 1.3f, this.transform.position.z), Quaternion.identity);
                        smb_coin.Play();
                    }
                    if (breakCount == 0)
                    {
                        // spawn the 4 small bricks
                        GameObject brick1 = Instantiate(breakableDownLeft, new Vector3(this.transform.position.x - 0.5f, this.transform.position.y + 0.3f, this.transform.position.z), Quaternion.identity); // bottom left
                        GameObject brick2 = Instantiate(breakableUpLeft, new Vector3(this.transform.position.x - 0.5f, this.transform.position.y - 0.3f, this.transform.position.z), Quaternion.identity); // top left
                        GameObject brick3 = Instantiate(breakableUpRight, new Vector3(this.transform.position.x + 0.5f, this.transform.position.y + 0.3f, this.transform.position.z), Quaternion.identity); // top right
                        GameObject brick4 = Instantiate(breakableDownRight, new Vector3(this.transform.position.x + 0.5f, this.transform.position.y - 0.3f, this.transform.position.z), Quaternion.identity); // bottom right

                        brick1.GetComponent<Rigidbody2D>().velocity = new Vector2(-4.0f, 2.0f);
                        brick2.GetComponent<Rigidbody2D>().velocity = new Vector2(-2.0f, 3.0f);
                        brick3.GetComponent<Rigidbody2D>().velocity = new Vector2(2.0f, 3.0f);
                        brick4.GetComponent<Rigidbody2D>().velocity = new Vector2(4.0f, 2.0f);

                        this.gameObject.SetActive(false);
                        breakCount = 5;
                    }
                    return;
                }
            }
        }
    }

}
