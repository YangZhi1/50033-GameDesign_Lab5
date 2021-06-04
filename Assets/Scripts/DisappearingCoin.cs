using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearingCoin : MonoBehaviour
{
    [SerializeField] private float duration = 0.5f;
    [SerializeField] private Rigidbody2D coinBody;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        coinBody.MovePosition(coinBody.position + new Vector2(0, 1.0f) * Time.fixedDeltaTime); 

        if(duration > 0)
        {
            duration -= Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
