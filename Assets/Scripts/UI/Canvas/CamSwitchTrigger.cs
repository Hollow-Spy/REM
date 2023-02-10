using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamSwitchTrigger : MonoBehaviour
{

    [SerializeField] CanvasScreenSplit splitter;
    [SerializeField] int CamNum;
   
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            splitter.SwitchMainCam(CamNum);
        }
    }
 
}
