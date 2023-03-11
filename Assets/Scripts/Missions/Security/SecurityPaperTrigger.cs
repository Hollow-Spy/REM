using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityPaperTrigger : MonoBehaviour
{
    [SerializeField] SlotShower slots;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            slots.SetItemUsable("Security Code", true);
        }
    }

   
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            slots.SetItemUsable("Security Code", false);

        }
    }
}
