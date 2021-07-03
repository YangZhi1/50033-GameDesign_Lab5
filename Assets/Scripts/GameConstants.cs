using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameConstants", menuName = "ScriptableObjects/GameConstants", order = 1)]
public class GameConstants : ScriptableObject
{
    // Set your data here

    // for Scoring system
    int currentScore;
    int currentPlayerHealth;

    // for Reset values
    Vector3 gombaSpawnPointStart = new Vector3(2.5f, -0.45f, 0); // hardcoded location
                                                                 // .. other reset values 

    // for Consume.cs
    public int consumeTimeStep = 10;
    public int consumeLargestScale = 4;

    // for Break.cs
    public int breakTimeStep = 30;
    public int breakDebrisTorque = 10;
    public int breakDebrisForce = 10;

    // for SpawnDebris.cs
    public int spawnNumberOfDebris = 10;

    // for Rotator.cs
    public int rotatorRotateSpeed = 6;

    // for testing
    public int testValue;


    public float groundSurface = -0.2f;

    public float enemySpeed = 4.0f;
    public float enemyDanceFrequency = 0.4f;

    public int startingNumberGoomba = 2;
    public int startingNumberGreenEnemy = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
