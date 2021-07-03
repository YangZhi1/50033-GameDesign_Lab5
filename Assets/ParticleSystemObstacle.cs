using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemObstacle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onGameRestart()
    {
        BreakBrickParticle[] bbp = FindObjectsOfType<BreakBrickParticle>();
        for(int i = 0; i < bbp.Length; i++)
        {
            bbp[i].onGameRestart();
        }
    }
}
