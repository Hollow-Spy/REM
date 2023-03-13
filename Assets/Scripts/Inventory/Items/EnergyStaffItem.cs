using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyStaffItem : MonoBehaviour
{
    [SerializeField] FontTyper Prompt;
    [SerializeField] string InspectMsg;
    [SerializeField] EnergyBoxInteractCard energyboxinteract;
    [SerializeField] InventoryEnabler inventory;
    public void Use()
    {
        energyboxinteract.CanActivate = true;
        inventory.SwitchUpInventory();
    }
    public void Inspect()
    {
        Prompt.Type(InspectMsg);
    }
}
