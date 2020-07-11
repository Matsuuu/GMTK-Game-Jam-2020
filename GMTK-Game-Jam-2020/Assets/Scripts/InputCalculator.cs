using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputCalculator : MonoBehaviour
{
    public InputManager inputs;
    
    public int movementInputCount;
    public int? maxMovementInputCount;
    public int jumpInputCount;
    public int? maxJumpInputCount;

    public Text movementCountTextElement;
    public Text jumpCountTextElement;
    // Start is called before the first frame update
    void Start()
    {
        inputs = GameObject.Find("GameManager").GetComponent<InputManager>();

    }

    // Update is called once per frame
    void Update()
    {
        CheckInputs();
    }

    private void CheckInputs()
    {
        if (Input.GetKeyDown(inputs.leftMove) || Input.GetKeyDown(inputs.rightMove))
        {
            movementInputCount++;
            movementCountTextElement.text = movementInputCount.ToString();
        }
        if (Input.GetKeyDown(inputs.jump))
        {
            jumpInputCount++;
            jumpCountTextElement.text = jumpInputCount.ToString();
        }
    }

    public void init(Nullable<int> movementInputs, Nullable<int> jumpInputs)
    {
        this.maxMovementInputCount = movementInputs;
        this.maxJumpInputCount = jumpInputs;
    }
}
