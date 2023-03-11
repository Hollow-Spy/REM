using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightCharmItem : MonoBehaviour
{
    [SerializeField] FontTyper Prompt;
    [SerializeField] string InspectMsg;
   
    [SerializeField] InventoryEnabler inventory;
    [SerializeField] GameObject LightOutMission;
    [SerializeField] SlotShower slots;
    bool Used;
    
    private void OnEnable()
    {
        
        slots.SetItemUsable("Light Charm", LightOutMission.activeSelf);     
    }
    public void Use()
    {
      if(!Used)
        {
            Used = true;
            inventory.SwitchUpInventory();

        }
    }
    public void Inspect()
    {
        Prompt.Type(InspectMsg);
    }
}
