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
    }

    public void IncrementJumpCounter()
    {
        jumpInputCount++;
        UpdateTextElements();
    }

    public void IncrementMovementCounter()
    {
        movementInputCount++;
        UpdateTextElements();
    }

    private void UpdateTextElements()
    {
        movementCountTextElement.text = movementInputCount + "/" + maxMovementInputCount;
        jumpCountTextElement.text = jumpInputCount + "/" + maxJumpInputCount;
    }

    public void Init(Nullable<int> movementInputs, Nullable<int> jumpInputs)
    {
        this.maxMovementInputCount = movementInputs;
        this.maxJumpInputCount = jumpInputs;
        UpdateTextElements();
    }

    public bool movementControlsExhausted()
    {
        return movementInputCount >= maxMovementInputCount;
    }

    public bool jumpControlsExhausted()
    {
        return jumpInputCount >= maxJumpInputCount;
    }
}
