using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private GameObject goombaPrefab;
    [SerializeField] private float[] goombaLocationX;

    private GameObject[] goombas;

    // Start is called before the first frame update
    void Start()
    {
        goombas = new GameObject[goombaLocationX.Length];
        for(int i=0; i < goombaLocationX.Length; i++)
        {
            goombas[i] = Instantiate(goombaPrefab, new Vector3(goombaLocationX[i], -0.282f, 0), Quaternion.identity);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onGameRestart()
    {
        goombas = new GameObject[goombaLocationX.Length];
        for (int i = 0; i < goombaLocationX.Length; i++)
        { 
            goombas[i] = Instantiate(goombaPrefab, new Vector3(goombaLocationX[i], -0.282f, 0), Quaternion.identity);
        }
    }

    public void KillAllGoombas()
    {
        foreach(GameObject goomba in goombas)
        {
            Destroy(goomba);
        }
    }

    public void StopGoombaMovement()
    {
        GoombaController[] allGoombas = FindObjectsOfType<GoombaController>();
        for(int i = 0; i < allGoombas.Length; i++)
        {
            allGoombas[i].StopGoombaMovement();
        }
    }
}
