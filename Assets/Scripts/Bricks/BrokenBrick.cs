using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenBrick : MonoBehaviour
{
    private int up = 1; // 0 for up, 1 for down
    private bool right;

    // Start is called before the first frame update
    void Start()
    {
        if (this.gameObject.name == "breakable_up_left" || this.gameObject.name == "breakable_up_72_left")
        {
            up = 0;
        }

        if(this.gameObject.name == "breakable_up_left" || this.gameObject.name == "breakable_down_left" || this.gameObject.name == "breakable_up_72_left" || this.gameObject.name == "breakable_down_72_left")
        {
            right = false;
        }
        else { right = true; }
    }

    // Update is called once per frame
    void Update()
    {
        if (right)
        {
            transform.Rotate(new Vector3(0, 0, -40.0f));
        }
        else
        {
            transform.Rotate(new Vector3(0, 0, 40.0f));
        }

        if(this.transform.position.y < -30f)
        {
            Destroy(this.gameObject);
        }
    }
}
