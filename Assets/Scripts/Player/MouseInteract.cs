using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MouseInteract : MonoBehaviour
{
    [SerializeField] Vector3 ScreenPos;
    [SerializeField] Vector3 worldPos;
    [SerializeField] LayerMask InteractMask;
    public Camera Cam;

    [SerializeField] RawImage img_screen;
    public Texture tex_;

    float OGResX, OGResY;

    [SerializeField] float divid;
    public bool OnScreen;
    bool wasSelecting;
    [SerializeField] float XOffset, YOffset;

    
   [SerializeField] CursorSwitch MouseScript;

    private void OnEnable()
    {
        OGResX = img_screen.rectTransform.rect.width;
        OGResY = img_screen.rectTransform.rect.height;
    }
  

    private void OnDisable()
    {
        if(wasSelecting)
        {
            wasSelecting = false;
            MouseScript.SwitchCursor();
        }
    }


    // Update is called once per frame
    void Update()
    {
        ScreenPos = Input.mousePosition;

        float resX = img_screen.rectTransform.rect.width;
        float resY = img_screen.rectTransform.rect.height;

        float div1 = resX / tex_.width;
        float div2 = resY / tex_.height;

        

        ScreenPos.x = ScreenPos.x / div1 + XOffset;
        ScreenPos.y = ScreenPos.y / div2 + YOffset;

        
        if(ScreenPos.x > tex_.width || ScreenPos.x < 0 || ScreenPos.y > tex_.height || ScreenPos.y < 0)
        {
            OnScreen = false;
        }
        else
        {
            OnScreen = true;
        }

        if(img_screen.rectTransform.rect.height != OGResY)
        {
            float offset;
            offset = OGResY - img_screen.rectTransform.rect.height;


            ScreenPos.y = ScreenPos.y - offset * divid;
        }

        if(OnScreen)
        {
            Ray ray = Cam.ScreenPointToRay(ScreenPos);
            if (Physics.Raycast(ray, out RaycastHit hitData, 100, InteractMask))
            {
                if(!wasSelecting)
                {
                    wasSelecting = true;
                    MouseScript.SwitchSelect();
                }


                if(Input.GetMouseButton(0))
                {
                    hitData.collider.gameObject.SendMessage("Interaction");
                }
                //worldPos = hitData.point;
            }
            else
            {
                if (wasSelecting)
                {
                    wasSelecting = false;
                    MouseScript.SwitchCursor();
                }
            }
          //  transform.position = worldPos;
        }
       
     
    }
}
