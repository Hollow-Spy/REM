using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandoScanButton : MonoBehaviour
{
    [SerializeField] GameObject[] DisableObjects;
    [SerializeField] GameObject[] EnableObjects;
    public void Interaction()
    {
        for (int i = 0; i < EnableObjects.Length; i++)
        {
            EnableObjects[i].SetActive(true);
        }
        for (int i=0;i<DisableObjects.Length;i++)
        {
            DisableObjects[i].SetActive(false);
        }
       

    }
    public void Interacted()
    {
        for (int i = 0; i < EnableObjects.Length; i++)
        {
            EnableObjects[i].SetActive(true);
        }
        for (int i = 0; i < DisableObjects.Length; i++)
        {
            DisableObjects[i].SetActive(false);
        }
    }
}
