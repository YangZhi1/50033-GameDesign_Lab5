using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void QuitButtonClicked()
    {
        StartCoroutine(ChangeScene("Level1"));
        Debug.Log("Quit button clicked");
    }

    public void PlayAgainButtonClicked()
    {
        Debug.Log("Play again button clicked");
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
