using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleCollider : MonoBehaviour
{
	public AudioSource changeSceneSound;

	// Start is called before the first frame update
	void Start()
    {
		foreach (Transform eachChild in transform)
		{
			eachChild.gameObject.SetActive(true);
		}
	}

    // Update is called once per frame
    void Update()
    {
        
    }

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			foreach(Transform eachChild in transform)
            {
				eachChild.gameObject.SetActive(true);
            }

            FindObjectOfType<AudioManager>().stopThemeSong();
			FindObjectOfType<EndFlagScript>().EndGame();
			changeSceneSound.PlayOneShot(changeSceneSound.clip);
			FindObjectOfType<PlayerController>().WinGame();
			StartCoroutine(LoadYourAsyncScene("MarioLevel2"));
		}
	}

	IEnumerator LoadYourAsyncScene(string sceneName)
	{
		yield return new WaitUntil(() => !changeSceneSound.isPlaying);
		CentralManager.centralManagerInstance.changeScene();
	}
}
