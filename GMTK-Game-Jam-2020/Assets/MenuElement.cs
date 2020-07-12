using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuElement : MonoBehaviour, IPointerEnterHandler
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Button btn;
    }

    private void HandleSelect()
    {
        Debug.Log("Select");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        HandleSelect();
    }
}
