using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource coin;
    [SerializeField] AudioSource stomp;
    [SerializeField] AudioSource jumpBig;
    [SerializeField] AudioSource jumpSmall;
    [SerializeField] AudioSource marioDie;
    [SerializeField] AudioSource pipeSound;
    [SerializeField] AudioSource marioScream;
    [SerializeField] AudioSource mushroom;
    [SerializeField] AudioSource themeSong;
    [SerializeField] AudioSource clear;
    [SerializeField] AudioSource shatter;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playClear()
    {
        themeSong.Stop();
        clear.Play();
    }

    public void playThemeSong()
    {
        themeSong.Play();
    }

    public void stopThemeSong()
    {
        themeSong.Stop();
    }

    public void mushroomBecomeBig()
    {
        mushroom.Play();
    }

    public void marioFall()
    {
        marioScream.Play();
    }

    public void collectCoin()
    {
        coin.Play();
    }

    public void marioDead()
    {
        marioDie.Play();
    }

    public void marioBigJump()
    {
        jumpBig.Play();
    }

    public void marioSmallJump()
    {
        jumpSmall.Play();
    }

    public void marioWarped()
    {
        pipeSound.Play();
    }

    public void killGoomba()
    {
        stomp.Play();
    }

    public void glassShatter()
    {
        shatter.Play();
    }
}
