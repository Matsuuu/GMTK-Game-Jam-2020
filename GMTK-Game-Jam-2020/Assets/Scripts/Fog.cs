﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fog : MonoBehaviour
{
    public float moveSpeed;
    public int fogWidth;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
    }
    
    private void OnBecameInvisible()
    {
        Vector3 pos = transform.position;
        transform.position = new Vector3(pos.x + fogWidth * 2, pos.y, pos.z);
    }
}
