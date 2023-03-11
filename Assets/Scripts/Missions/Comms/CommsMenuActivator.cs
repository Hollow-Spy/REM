using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommsMenuActivator : MonoBehaviour
{
    [SerializeField] CanvasPopUpTrigger poptrigger;
    [SerializeField] CanvasScreenSplit splitter;
    [SerializeField] GameObject MenuButton;
    void Start()
    {
        splitter.PopUpText("Who is this?", poptrigger.PopNum);
        Invoke("MenuButtonActivate", 3);
    }
    
   void MenuButtonActivate()
    {
        splitter.PopUpText("", poptrigger.PopNum);
        MenuButton.SetActive(true);
        gameObject.SetActive(false);
    }
}
