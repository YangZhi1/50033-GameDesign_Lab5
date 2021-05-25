using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{
    bool gameHasEnded = false;
    public Text gameOverText;
    public Button restartButton;

    public Transform player;
    public Transform enemy;

    public GameObject coinPrefab;
    private GameObject newCoin;

    private void Start()
    {
        gameOverText.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
        SpawnCoin();
        StartCoroutine(createCoin());
    }
    public void EndGame()
    {
        if (gameHasEnded == false)
        {
            // show game over text and restart button
            gameOverText.gameObject.SetActive(true);
            restartButton.gameObject.SetActive(true);

            gameHasEnded = true;
        }

    }

    public void Restart()
    {
        // SceneManager.LoadScene("SampleScene");
        gameOverText.gameObject.SetActive(false);

        gameHasEnded = false;

        restartButton.gameObject.SetActive(false);
    }

    private void SpawnCoin()
    {
        newCoin = Instantiate(coinPrefab) as GameObject;
        newCoin.transform.position = new Vector2(Random.Range(-6.0f, 6.0f), 4.15f);
    }

    IEnumerator createCoin()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.5f);
            if (!newCoin.gameObject)
            {
                SpawnCoin();
            }
        }
    }

}
