using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakBrickParticle : MonoBehaviour
{
    private bool broken = false;
    [SerializeField] GameObject prefab; // debris prefab
    [SerializeField] GameObject disappearingCoin;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player") && !broken)
        {
            // give player coin when hit
            GameObject newCoin = Instantiate(disappearingCoin, new Vector3(this.transform.position.x, this.transform.position.y + 1.3f, this.transform.position.z), Quaternion.identity);
            FindObjectOfType<PlayerController>().marioCollectCoin();

            // code to spawn a random enemy
            if (Random.Range(0,2) == 0) { SpawnManager.SpawnManagerInstance.spawnFromPooler(ObjectType.gombaEnemy); }
            else { SpawnManager.SpawnManagerInstance.spawnFromPooler(ObjectType.greenEnemy); }

            broken = true;
            FindObjectOfType<AudioManager>().glassShatter();
            // assume we have 5 debris per box
            for (int x = 0; x < 5; x++)
            {
                Instantiate(prefab, transform.position, Quaternion.identity);
            }
            gameObject.transform.parent.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.transform.parent.GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<EdgeCollider2D>().enabled = false;
        }
    }

    public void onGameRestart()
    {
        GetComponent<EdgeCollider2D>().enabled = true;
        gameObject.transform.parent.GetComponent<SpriteRenderer>().enabled = true;
        gameObject.transform.parent.GetComponent<BoxCollider2D>().enabled = true;
        broken = false;
    }
}
