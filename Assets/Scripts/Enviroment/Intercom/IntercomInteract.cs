using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntercomInteract : MonoBehaviour
{
    
     CanvasScreenSplit splitter;
    [SerializeField] int PopNum;
    bool CanActivate,isPowered,TextPoped;

    private void Start()
    {
        splitter = GameObject.FindGameObjectWithTag("GameController").GetComponent<CanvasScreenSplit>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CanActivate = true;
        }
    }
    private void Update()
    {
        if(CanActivate && !splitter.GetisCensored(PopNum))
        {
            if (isPowered && Input.GetKeyDown(KeyCode.Space))
            {
             
            }
            else
            {
                if (!TextPoped)
                {
                    TextPoped = true;
                    splitter.PopUpText("There is no power", PopNum);

                }
            }
        }
   
     
       
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TextPoped = false;

            CanActivate = false;

        }
    }
}
