using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableSwitchTrigger : MonoBehaviour
{
    [SerializeField] bool Activate;
    [SerializeField] GameObject[] objects;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            for(int i=0;i< objects.Length;i++)
            {
                objects[i].SetActive(Activate);
            }
        }
    }
}

