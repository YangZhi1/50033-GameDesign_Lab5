using UnityEngine;

public class GameUI : Singleton<GameUI>
{
    override public void Awake()
    {
        base.Awake();
        Debug.Log("awake called");
        // other instructions...
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
