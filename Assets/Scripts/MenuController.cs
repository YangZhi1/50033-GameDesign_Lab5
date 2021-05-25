using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField] private AudioSource themeSong;
    public static bool isDead = false;

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
                Debug.Log("Child found. Name: " + eachChild.name);
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
        isDead = false;
        EnemyController.restartedGame = true;
        PlayerController.restartedGame = true;
        FindObjectOfType<GameManager>().Restart();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            themeSong.Stop();
        }
    }
}
