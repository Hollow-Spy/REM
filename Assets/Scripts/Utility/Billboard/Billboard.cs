using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    CanvasScreenSplit splitter;
    private void Start()
    {
        splitter = FindObjectOfType<CanvasScreenSplit>();
    }
    void Update()
    {
     if(splitter.CurrentRoomCamera != null)
        {
            transform.LookAt(splitter.CurrentRoomCamera.transform);
        }
    }
}
