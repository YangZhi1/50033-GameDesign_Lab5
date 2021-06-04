using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenBrick : MonoBehaviour
{
    private int up = 1; // 0 for up, 1 for down
    // Start is called before the first frame update
    void Start()
    {
        if (this.gameObject.name == "breakable_up" || this.gameObject.name == "breakable_up_72")
        {
            up = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(this.transform.position.y < -30f)
        {
            Destroy(this.gameObject);
        }
    }
}
