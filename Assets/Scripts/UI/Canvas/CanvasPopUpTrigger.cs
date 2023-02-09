using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasPopUpTrigger : MonoBehaviour
{

    [SerializeField] CanvasScreenSplit Splitter;
    [SerializeField] int PopNum;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Splitter.PopUp(PopNum);
        
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Splitter.PopDown(PopNum);

        }
    }
}
