using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorSwitch : MonoBehaviour
{
    [SerializeField] Camera MainCam;

    [SerializeField] Image CursorImg, SelectImg;

    [SerializeField] GameObject ClickSound,ClickOutSound;
    Color DefaultColor;
    [SerializeField] Color PressColor;
    
    private void Start()
    {
        DefaultColor = CursorImg.color;
        Cursor.visible = false;
    }
    public void SwitchSelect()
    {
        CursorImg.enabled = false;
        SelectImg.enabled = true;
    }
    public void SwitchCursor()
    {
        CursorImg.enabled = true ;
        SelectImg.enabled = false;
    }


    void Update()
    {
        
        transform.position = Input.mousePosition;
        if(Input.GetMouseButtonDown(0))
        {
            Instantiate(ClickSound, transform.position, Quaternion.identity);
            CursorImg.transform.localScale = new Vector3(.9f, .9f, .9f);
            CursorImg.color = PressColor;
            SelectImg.color = PressColor;

        }
        if(Input.GetMouseButtonUp(0))
        {
            Instantiate(ClickOutSound, transform.position, Quaternion.identity);
            CursorImg.transform.localScale = new Vector3(1,1,1);
            CursorImg.color = DefaultColor;
            SelectImg.color = DefaultColor;

        }
    }

  
}
