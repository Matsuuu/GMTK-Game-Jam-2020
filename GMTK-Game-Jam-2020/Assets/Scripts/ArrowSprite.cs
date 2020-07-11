using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSprite : MonoBehaviour
{
    private SpriteRenderer mySprite;
    public Sprite unheldSprite;
    public Sprite heldSprite;
    public KeyCode keyCode;
    // Start is called before the first frame update
    void Start()
    {
        mySprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(keyCode))
        {
            mySprite.sprite = heldSprite;
        } else
        {
            mySprite.sprite = unheldSprite;
        }
    }
}
