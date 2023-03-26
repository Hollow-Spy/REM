using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteract : MonoBehaviour
{
    [SerializeField] CanvasPopUpTrigger CanvasTrigger;
    [SerializeField] DoorOpener doorScript;
    bool CanActivate;
    
    public void Interaction()
    {
       
        if (CanvasTrigger.canActivate && !CanvasTrigger.Splitter.GetisCensored(CanvasTrigger.PopNum))
        {
           
            doorScript.Interact();
            if(doorScript.isBlocked)
            {
               CanvasTrigger.Splitter.PopUpText("Door is Locked", CanvasTrigger.PopNum);
            }
            if(doorScript.BotOpenDelay)
            {
                CanvasTrigger.Splitter.PopUpText("It's not working", CanvasTrigger.PopNum);
            }
        }
    }
  
   
}
