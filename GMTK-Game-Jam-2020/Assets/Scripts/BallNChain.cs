using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallNChain : MonoBehaviour
{
    public float animationSpeed;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.speed = animationSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
