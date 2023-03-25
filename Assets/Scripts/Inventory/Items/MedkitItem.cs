using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedkitItem : MonoBehaviour
{
    [SerializeField] FontTyper Prompt;
    [SerializeField] string InspectMsg;
    [SerializeField] PlayerHealth playerhealth;
    [SerializeField] InventoryEnabler inventory;
    [SerializeField] SlotShower slots;
    [SerializeField] GameObject UseSound;
   
    private void OnEnable()
    {
        if(playerhealth.Health < 3)
        {
            for (int i = 0; i < slots.ItemsStored.Length; i++)
            {
                if (slots.ItemsStored[i].Contains("Medkit"))
                {
                    slots.SetItemUsable(slots.ItemsStored[i], true);
                }
            }
        }
     
    }

    public void Use()
    {
        Instantiate(UseSound, transform.position, Quaternion.identity);
        playerhealth.Heal();
        slots.RemoveItem(gameObject.name);
        inventory.SwitchUpInventory();
      
    }
    public void Inspect()
    {
        Prompt.Type(InspectMsg);
    }
}
