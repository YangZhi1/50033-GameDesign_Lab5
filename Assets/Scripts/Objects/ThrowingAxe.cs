using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingAxe : MonoBehaviour
{
    private bool faceRight;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (faceRight)
        {
            transform.Rotate(new Vector3(0, 0, -20.0f));
        }

        else
        {
            transform.Rotate(new Vector3(0, 0, 20.0f));
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
    public void setDirection(bool dir)
    {
        faceRight = dir;
    }
}
