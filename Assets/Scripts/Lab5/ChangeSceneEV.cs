using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ChangeSceneEV : MonoBehaviour
{
    public AudioSource changeSceneSound;
    private bool checkHit = false;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (!checkHit)
            {
                changeSceneSound.PlayOneShot(changeSceneSound.clip);
                checkHit = true;
                FindObjectOfType<AudioManagerEV>().stopTheme();
                StartCoroutine(WaitSoundClip("MarioGameEVLevel2"));
            }
            //StartCoroutine(ChangeScene("MarioGameEVLevel2"));
        }
    }

    IEnumerator WaitSoundClip(string sceneName)
    {
        yield return new WaitUntil(() => !changeSceneSound.isPlaying);
        StartCoroutine(ChangeScene(sceneName));

    }
    IEnumerator ChangeScene(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}