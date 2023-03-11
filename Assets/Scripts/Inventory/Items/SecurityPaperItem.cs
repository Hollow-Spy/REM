using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityPaperItem : MonoBehaviour
{
    
    [SerializeField] FontTyper Prompt;
    [SerializeField] string InspectMsg;
    [SerializeField] GameObject ConfirmationInteract;
    [SerializeField] InventoryEnabler inventory;
   public void Use()
    {
        ConfirmationInteract.SetActive(true);
        inventory.SwitchUpInventory();
    }
    public void Inspect()
    {
        Prompt.Type(InspectMsg);
    }
}
