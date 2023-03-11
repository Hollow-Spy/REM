using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryAdd : MonoBehaviour
{

    [SerializeField] GameObject PickUpSound;
    [SerializeField] SlotShower Slots;
    [SerializeField] string ItemName;
    public void Interaction()
    {
        Instantiate(PickUpSound, transform.position, Quaternion.identity);
        Slots.AddItem(ItemName);
      
        gameObject.SetActive(false);
    }
}
