using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{
    bool gameHasEnded = false;

    public GameObject coinPrefab;
    private GameObject newCoin;

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

}
