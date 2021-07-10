using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SceneTwoUpdateScore : MonoBehaviour
{
    public UnityEvent sceneChange;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Invoking scene change");
        sceneChange.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
