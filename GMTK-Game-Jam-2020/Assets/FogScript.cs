using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogScript : MonoBehaviour
{
    
    public float speed = 0.01f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 260);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(transform.position.x - speed, transform.position.y);
    }
}
