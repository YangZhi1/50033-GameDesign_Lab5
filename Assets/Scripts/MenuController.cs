using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] private Button ruleButton;
    [SerializeField] private GameObject homeScreen;
    [SerializeField] private GameObject nothingHere;

    private void Awake()
    {
        Time.timeScale = 0.0f;
        foreach(Transform eachChild in transform)
        {
            if(eachChild.name == "Panel")
            {
                eachChild.gameObject.SetActive(false);
            }
        }
    }

    public void StartButtonClicked()
    {
        foreach(Transform eachChild in transform)
        {
            if(eachChild.name != "Score" && eachChild.name != "CoinCount" && eachChild.name != "QuitButton")
            {
                //Debug.Log("Child found. Name: " + eachChild.name);
                //disable them
                eachChild.gameObject.SetActive(false);
                Time.timeScale = 1.0f;
            }
        }

        FindObjectOfType<AudioManager>().playThemeSong();
    }

    public void QuitButtonClicked()
    {
        stopSong();
        FindObjectOfType<GameManager>().EndGame();
        FindObjectOfType<PlayerController>().StopMarioMovements();
        FindObjectOfType<EnemyController>().StopGoombaMovement();
    }
    
    public void RestartButtonClicked()
    {
        FindObjectOfType<AudioManager>().playThemeSong();
        FindObjectOfType<GameManager>().Restart();
        FindObjectOfType<PlayerController>().RestartGame();
        FindObjectOfType<EnemyController>().KillAllGoombas();
        FindObjectOfType<EnemyController>().onGameRestart();

        ObstacleController[] obstacleC = FindObjectsOfType<ObstacleController>();
        QuestionBoxController[] qbc = FindObjectsOfType<QuestionBoxController>();
        for(int i = 0; i < obstacleC.Length; i++)
        {
            obstacleC[i].onGameRestart();
        }

        for (int i = 0; i < qbc.Length; i++)
        {
            qbc[i].onGameRestart();
        }

        FindObjectOfType<ConsummableMushroom>().onGameRestart();
    }

    public void RuleButtonClicked()
    {
        GameObject theRules = gameObject.transform.Find("Rules").gameObject;
        theRules.SetActive(true);

        GameObject menuScreen = gameObject.transform.Find("SuperMarioBrosLogo").gameObject;
        menuScreen.SetActive(false);
    }

    public void RuleBackButtonClicked()
    {
        GameObject theRules = gameObject.transform.Find("Rules").gameObject;
        theRules.SetActive(false);

        GameObject menuScreen = gameObject.transform.Find("SuperMarioBrosLogo").gameObject;
        menuScreen.SetActive(true);
    }

    public void stopSong()
    {
        FindObjectOfType<AudioManager>().stopThemeSong();
    }

    public void showHomeScreen()
    {
        homeScreen.gameObject.SetActive(true);
    }

    public void nothingToSeeHere()
    {
        nothingHere.gameObject.SetActive(true);
    }

    public void hideNothingToSeeHere()
    {
        nothingHere.gameObject.SetActive(false);
    }
}
