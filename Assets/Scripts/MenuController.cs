using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField] private AudioSource themeSong;

    private void Awake()
    {
        Time.timeScale = 0.0f;
    }

    public void StartButtonClicked()
    {
        foreach(Transform eachChild in transform)
        {
            if(eachChild.name != "Score")
            {
                //Debug.Log("Child found. Name: " + eachChild.name);
                //disable them
                eachChild.gameObject.SetActive(false);
                Time.timeScale = 1.0f;
            }
        }

        themeSong.Play();
    }
    
    public void RestartButtonClicked()
    {
        themeSong.Play();
        FindObjectOfType<GameManager>().Restart();
        FindObjectOfType<PlayerController>().RestartGame();
        FindObjectOfType<EnemyController>().ResetGombaPosition();
    }

    public void stopSong()
    {
        themeSong.Stop();
    }
}
