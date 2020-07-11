using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentDataManager : MonoBehaviour
{
    public struct StageTime
    {
        public  int minutes { get; set; }
        public int seconds { get; set; }
        public StageTime(int minutes, int seconds)
        {
            this.minutes = minutes;
            this.seconds = seconds;
        }
    }
    public int currentStage = 1;

    public StageTime stageTime;
    private float startTime;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartTime()
    {
        startTime = Time.time;
    }

    public void EndTime()
    {
        float timeTaken = Time.time - startTime;

        int minutes = (int) Mathf.Floor(timeTaken / 60);
        int seconds = (int)timeTaken % 60;
        stageTime = new StageTime(minutes, seconds);
        Debug.Log("Finished with time: " + minutes + " minutes, " + seconds + " seconds.");
    }
}
