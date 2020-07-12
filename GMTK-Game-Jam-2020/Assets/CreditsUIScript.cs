using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditsUIScript : MonoBehaviour
{
    public SpriteRenderer mainMenuImage;
    public Canvas mainMenuCanvas;
    public Image creditsImage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowCredits()
    {
        mainMenuImage.enabled = false;
        mainMenuCanvas.enabled = false;
        creditsImage.enabled = true;
    }

    public void LeaveCredits()
    {
        mainMenuImage.enabled = true;
        mainMenuCanvas.enabled = true;
        creditsImage.enabled = false;
    }
}
