using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUsableTrigger : MonoBehaviour
{
    [SerializeField] SlotShower slots;
    [SerializeField] string ItemName;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            slots.SetItemUsable(ItemName, true);
        }
    }

   
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            slots.SetItemUsable(ItemName, false);

        }
    }
}
