using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteract : MonoBehaviour
{
    [SerializeField] DoorOpener doorScript;
    [SerializeField] CanvasScreenSplit splitter;
    [SerializeField] int PopNum;
    bool CanActivate;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CanActivate = true;
        }
    }
    private void Update()
    {
        if (CanActivate && Input.GetKeyDown(KeyCode.Space) && !splitter.GetisCensored(PopNum))
        {
            doorScript.Interact();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CanActivate = false;

        }
    }
}
