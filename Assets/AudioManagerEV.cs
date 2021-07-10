using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerEV : MonoBehaviour
{
    public AudioSource marioTheme;

    // Start is called before the first frame update
    void Start()
    {
        playTheme();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playTheme()
    {
        marioTheme.Play();
    }

    public void stopTheme()
    {
        marioTheme.Stop();
    }
}
