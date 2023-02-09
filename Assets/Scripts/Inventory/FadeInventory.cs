using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInventory : MonoBehaviour
{


    public void FadingFalse()
    {
        InventoryEnabler.Fading = false;
    }
    public void FadeEnd()
    {
        InventoryEnabler.FadeEnd = true;
    }
  
}
