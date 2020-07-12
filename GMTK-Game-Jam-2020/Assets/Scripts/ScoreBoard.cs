using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{
    public Text stageNameTitle;
    public Text jumpCountTitle;
    public Text movementCountTitle;
    public Canvas canvas;
    public SpriteRenderer spriteRenderer;
    public PersistentDataManager persistentDataManager;
    public InputManager inputs;

    public bool showing = false;
    // Start is called before the first frame update
    void Start()
    {
        inputs = GameObject.Find("GameManager").GetComponent<InputManager>();
        persistentDataManager = GameObject.Find("PersistentDataManager").GetComponent<PersistentDataManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (showing && Input.GetKeyDown(inputs.jump))
        {
            persistentDataManager.NextStage();
        }
    }

    public void Show()
    {
        stageNameTitle.text = "Stage " + persistentDataManager.currentStage + " completed";
        jumpCountTitle.text = persistentDataManager.usedJumps + "/" + persistentDataManager.maxJumps + " Jump Control Commands issued";
        movementCountTitle.text = persistentDataManager.usedMovements + "/" + persistentDataManager.maxMovements + " Movement Control Commands issued";

        canvas.enabled = true;
        spriteRenderer.enabled = true;
        showing = true;
    }
}
