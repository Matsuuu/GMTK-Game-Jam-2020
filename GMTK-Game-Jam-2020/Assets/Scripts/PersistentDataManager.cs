using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistentDataManager : MonoBehaviour
{
    private InputCalculator inputCalculator;
    public int? maxJumps;
    public int? usedJumps;
    public int? maxMovements;
    public int? usedMovements;

    private List<String> stageNames = new List<String>()
    {
        "MenuScene",
        "TutorialStage", 
        "SecondStage",
         "OnlyRun",
         "ThirdStage",
         "FinalScene",
         "EndScene"
    };
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
        Init();
        
    }

    void Init()
    {
        GameObject gameManager = GameObject.Find("GameManager");
        if (gameManager)
        {
            inputCalculator = gameManager.GetComponent<InputCalculator>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public String GetCurrentStageName()
    {
        return stageNames[currentStage];
    }

    public void GoToStage(String name)
    {
        currentStage = stageNames.FindIndex(n => n == name);
        SceneManager.LoadScene(name);
    }

    public void GoToStage(int order)
    {
        currentStage = order;
        SceneManager.LoadScene(stageNames[order]);
    }

    public void NextStage()
    {
        currentStage++;
        GoToStage(GetCurrentStageName());
    }

    public void StartTime()
    {
        startTime = Time.time;
    }

    public void EndTime()
    {
        Init();
        float timeTaken = Time.time - startTime;

        int minutes = (int) Mathf.Floor(timeTaken / 60);
        int seconds = (int)timeTaken % 60;
        stageTime = new StageTime(minutes, seconds);

        maxJumps = inputCalculator.maxJumpInputCount;
        usedJumps = inputCalculator.jumpInputCount;
        maxMovements = inputCalculator.maxMovementInputCount;
        usedMovements = inputCalculator.movementInputCount;
        
        Debug.Log("Finished with time: " + minutes + " minutes, " + seconds + " seconds.");
        StartCoroutine(EndTimeout());
    }

    private IEnumerator EndTimeout()
    {
        yield return new WaitForSeconds(2);
        GameObject winningBlur = GameObject.Find("WinningBlur");
        winningBlur.GetComponent<ScoreBoard>().Show();
        yield return new WaitForSeconds(3);
    }
}
