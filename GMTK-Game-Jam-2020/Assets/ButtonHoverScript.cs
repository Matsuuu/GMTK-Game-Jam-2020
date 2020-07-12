using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonHoverScript : MonoBehaviour
{
    private Image image;
    public Sprite noHoverSprite;
    public Sprite hoverSprite;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HandleEnter()
    {
        image.sprite = hoverSprite;
    }

    public void HandleExit()
    {
        image.sprite = noHoverSprite;
    }
}
