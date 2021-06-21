using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableTile : MonoBehaviour
{
    [SerializeField] private GameObject breakableDownLeft;
    [SerializeField] private GameObject breakableDownRight;
    [SerializeField] private GameObject breakableUpLeft;
    [SerializeField] private GameObject breakableUpRight;
    private bool isDead = false;
    private float deadTime = 5.0f; // time before we destroy this gameObject after it is hit by axe

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            if(!(deadTime < 0))
            {
                deadTime -= Time.deltaTime;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("FlyingAxe"))
        {
            isDead = true;
            // spawn the 4 small bricks
            GameObject brick1 = Instantiate(breakableDownLeft, new Vector3(this.transform.position.x - 0.5f, this.transform.position.y + 0.3f, this.transform.position.z), Quaternion.identity); // bottom left
            GameObject brick2 = Instantiate(breakableDownLeft, new Vector3(this.transform.position.x - 0.5f, this.transform.position.y - 0.3f, this.transform.position.z), Quaternion.identity); // top left
            GameObject brick3 = Instantiate(breakableUpRight, new Vector3(this.transform.position.x + 0.5f, this.transform.position.y + 0.3f, this.transform.position.z), Quaternion.identity); // top right
            GameObject brick4 = Instantiate(breakableUpRight, new Vector3(this.transform.position.x + 0.5f, this.transform.position.y - 0.3f, this.transform.position.z), Quaternion.identity); // bottom right

            brick1.GetComponent<Rigidbody2D>().velocity = new Vector2(-4.0f, 2.0f);
            brick2.GetComponent<Rigidbody2D>().velocity = new Vector2(-2.0f, 3.0f);
            brick3.GetComponent<Rigidbody2D>().velocity = new Vector2(2.0f, 3.0f);
            brick4.GetComponent<Rigidbody2D>().velocity = new Vector2(4.0f, 2.0f);

            gameObject.SetActive(false);
        }
    }
}
