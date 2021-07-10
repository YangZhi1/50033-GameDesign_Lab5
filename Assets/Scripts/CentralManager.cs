using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CentralManager : MonoBehaviour
{
	public GameObject gameManagerObject;
	private GameManager gameManager;
	public static CentralManager centralManagerInstance;

	// add reference to PowerupManager
	public GameObject powerupManagerObject;
	private PowerUpManager powerUpManager;

	void Awake()
	{
		centralManagerInstance = this;
	}
	// Start is called before the first frame update
	void Start()
	{
		// instantiate in start
		powerUpManager = powerupManagerObject.GetComponent<PowerUpManager>();
		gameManager = gameManagerObject.GetComponent<GameManager>();
	}

	public void increaseScore(int val)
	{
		gameManager.increaseScore(val);
	}

	public void damagePlayer()
	{
		gameManager.damagePlayer();
	}

	public void consumePowerup(KeyCode k, GameObject g)
	{
		powerUpManager.consumePowerup(k, g);
	}

	public void addPowerup(Texture t, int i, ConsumableInterface c)
	{
		powerUpManager.addPowerup(t, i, c);
	}

	public void resetGame()
	{
		gameManager.resetGame();
		//ObjectPooler.SharedInstance.onGameRestart();

		GoombaController[] allEnemies = FindObjectsOfType<GoombaController>();
		foreach (GoombaController enemy in allEnemies)
		{
			enemy.ResetEnemy();
		}
	}

	public void resetScore()
	{
		gameManager.resetScore();
	}

	public void changeScene()
	{
		StartCoroutine(LoadYourAsyncScene("Level 2"));
	}

	IEnumerator LoadYourAsyncScene(string sceneName)
	{
		// The Application loads the Scene in the background as the current Scene runs.
		// This is particularly good for creating loading screens.
		AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
		// Wait until the asynchronous scene fully loads
		while (!asyncLoad.isDone)
		{
			yield return null;
		}
	}
}
