using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryEnabler : MonoBehaviour
{


    bool OnScreen = false, busy = false;
  public  static bool Fading = false, FadeEnd;
    [SerializeField] GameObject InventoryObject,BlackFadeObject;
    [SerializeField] SlotShower Slots;
    
   
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            SwitchUpInventory();
        }
    }

    public void SwitchUpInventory()
    {
        if(!busy)
        {
            Fading = true;
            FadeEnd = false;
            busy = true;
            OnScreen = !OnScreen;
            BlackFadeObject.SetActive(true);
            StartCoroutine(Switch());

        }


    }



    IEnumerator Switch()
    {
        while(Fading)
        {
            yield return null;

        }
       
        InventoryObject.SetActive(OnScreen);
        if(OnScreen)
        {
            Slots.OrganizeSlots();
        }

        while (!FadeEnd)
        {
            yield return null;

        }
        BlackFadeObject.SetActive(false);
        yield return new WaitForSeconds(.1f);
        busy = false;

    }
}
