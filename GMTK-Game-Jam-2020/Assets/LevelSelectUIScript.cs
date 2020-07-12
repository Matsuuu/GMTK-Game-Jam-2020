using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectUIScript : MonoBehaviour
{
    public Canvas mainMenuCanvas;
    public Canvas levelsCanvas;
    public SpriteRenderer levelsImage;
    public SpriteRenderer mainMenuImage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToLevelSelectUI()
    {
        mainMenuCanvas.enabled = false;
        mainMenuImage.enabled = false;
        levelsCanvas.enabled = true;
        levelsImage.enabled = true;
    }

    public void ExitLevelSelectUI()
    {
        mainMenuCanvas.enabled = true;
        mainMenuImage.enabled = true;
        levelsCanvas.enabled = false;
        levelsImage.enabled = false;
    }
}
