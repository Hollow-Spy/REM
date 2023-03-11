using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommsPickUp : MonoBehaviour
{
    [SerializeField] CommsManager manager;
    [SerializeField] GameObject MenuActivator;
    public void Interaction()
    {
        manager.RecievedCall();
        MenuActivator.SetActive(true);
        gameObject.SetActive(false);
    }
}
