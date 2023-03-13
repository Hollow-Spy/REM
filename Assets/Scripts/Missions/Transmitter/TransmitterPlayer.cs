using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransmitterPlayer : MonoBehaviour
{
    [SerializeField] IntercomInteract intercom;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Transmitter"))
        {
            intercom.Lost();
        }
    }
}
