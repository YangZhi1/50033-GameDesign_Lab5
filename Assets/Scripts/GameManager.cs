using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class GameManager : Singleton<GameManager>
{
    

    bool gameHasEnded = false;

    public GameObject coinPrefab;
    private GameObject newCoin;
    public Text score;
    private int playerScore = 0;

    public delegate void gameEvent();
    public static event gameEvent OnPlayerDeath;
    public static event gameEvent OnGameReset;


    // Singleton Pattern
    private static GameManager _instance;
    // Getter
    public static GameManager Instance
    {
        get { return _instance; }
    }

    override public void Awake()
    {
        base.Awake();
        Debug.Log("awake called");
        // other instructions...
    }

    private void Start()
    {

    }
    public void EndGame()
    {
        if (gameHasEnded == false)
        {
            // show game over text and restart button
            FindObjectOfType<CameraController>().GameOverUpdate();
            FindObjectOfType<MenuController>().showHomeScreen();
            gameHasEnded = true;
        }

    }

    public void Restart()
    {
        // SceneManager.LoadScene("SampleScene");

        gameHasEnded = false;
    }

    public void increaseScore(int val)
    {
        playerScore += val;
        score.text = playerScore.ToString();
    }

    public void resetScore()
    {
        score.text = "0";
    }

    public void damagePlayer()
    {
        OnPlayerDeath();
    }

    public void resetGame()
    {
        OnGameReset();
    }
}
