using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public int maxMovementCount;
    public int maxJumpCount;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<InputCalculator>().Init(maxMovementCount, maxJumpCount);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
