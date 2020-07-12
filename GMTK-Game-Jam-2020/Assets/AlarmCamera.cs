using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmCamera : MonoBehaviour
{
    Camera mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GetComponent<Camera>();
        StartCoroutine(LerpColors());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator LerpColors()
    {
        Color black = new Color(0.21875f, 0.21875f, 0.21875f);
        Color red = new Color(0.488f, 0.1406f, 0.1406f);
        bool toRed = true;
        int iterations = 0;
        while(true)
        {
            if (toRed)
            {
                mainCamera.backgroundColor = Color.Lerp(black, red, Mathf.PingPong(Time.time, 1));
            } else
            {
                mainCamera.backgroundColor = Color.Lerp(red, black, Mathf.PingPong(Time.time, 1));

            }
            iterations++;
            if (iterations > 120)
            {
                iterations = 0;
                toRed = !toRed;
            }
            yield return new WaitForSeconds(0.1f);
        }

    }
}
