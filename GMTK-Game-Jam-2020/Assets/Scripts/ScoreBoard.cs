using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{
    public Text stageNameTitle;
    public Text jumpCountTitle;
    public Text movementCountTitle;
    public PersistentDataManager persistentDataManager;
    // Start is called before the first frame update
    void Start()
    {
        persistentDataManager = GameObject.Find("PersistentDataManager").GetComponent<PersistentDataManager>();
        stageNameTitle.text = "Stage " + persistentDataManager.currentStage + " completed";
        jumpCountTitle.text = persistentDataManager.usedJumps + "/" + persistentDataManager.maxJumps + " Jump Control Commands issued";
        movementCountTitle.text = persistentDataManager.usedMovements + "/" + persistentDataManager.maxMovements + " Movement Control Commands issued";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
