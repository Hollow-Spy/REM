using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasPopUpTrigger : MonoBehaviour
{

    public CanvasScreenSplit Splitter;
    public int PopNum;
    public bool canActivate;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            canActivate = true;
            Splitter.PopUp(PopNum);
        
        }
    }
    private void OnDisable()
    {
       if(canActivate)
        {
            canActivate = false;
            Splitter.PopDown(PopNum);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canActivate = false;
            Splitter.PopDown(PopNum);

        }
    }
}
