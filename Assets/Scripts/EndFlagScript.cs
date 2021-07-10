using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndFlagScript : MonoBehaviour
{
    [SerializeField] private Rigidbody2D flagBody;
    private bool moveFlag = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (moveFlag)
        {
            Debug.Log(flagBody.position.y);
            if (flagBody.position.y < 6.0f)
            {
                flagBody.MovePosition(flagBody.position + new Vector2(0, 1.0f) * Time.fixedDeltaTime);
            }
        }
        else
        {
            moveFlag = false;
        }
    }

    public void EndGame()
    {
        Debug.Log("flag moving up");
        moveFlag = true;
    }

    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            moveFlag = true;

            FindObjectOfType<AudioManager>().stopThemeSong();
            FindObjectOfType<AudioManager>().playClear();
            FindObjectOfType<GameManager>().EndGame();
            FindObjectOfType<PlayerController>().StopMarioMovements();
            //FindObjectOfType<GoombaController>().StopGoombaMovement();    
        }
    }*/
}
