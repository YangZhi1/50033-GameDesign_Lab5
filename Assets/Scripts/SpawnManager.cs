using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager SpawnManagerInstance;
    public GameConstants gameConstants;

    private void Awake()
    {
        SpawnManagerInstance = this;
        // spawn gombaEnemy
        for (int j = 0; j < gameConstants.startingNumberGoomba; j++)
            spawnFromPooler(ObjectType.gombaEnemy);
        // spawn snail
        for (int j = 0; j < gameConstants.startingNumberGreenEnemy; j++)
            spawnFromPooler(ObjectType.greenEnemy);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void spawnFromPooler(ObjectType i)
    {
        // static method access
        GameObject item = ObjectPooler.SharedInstance.GetPooledObject(i);
        if (item != null)
        {
            //set position, and other necessary states
            item.transform.position = new Vector3(Random.Range(10.5f, 20.5f), item.transform.position.y, 0);
            item.SetActive(true);
        }
        else
        {
            Debug.Log("not enough items in the pool.");
        }
    }

    public void spawnOnDeath(ObjectType i, float xLocation)
    {
        // static method access
        for (int index = 0; index < 2; index++)
        {
            GameObject item = ObjectPooler.SharedInstance.GetPooledObject(i);
            if (item != null)
            {
                //set position, and other necessary states
                item.transform.position = new Vector3(xLocation + index, item.transform.position.y, 0);
                item.SetActive(true);
                item.GetComponent<GoombaController>().ResetEnemy();
            }
            else
            {
                Debug.Log("not enough items in the pool.");
            }
        }
    }
}
