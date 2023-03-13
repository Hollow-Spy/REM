using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightCharmDestroy : MonoBehaviour
{
    [SerializeField] GameObject LightCharm;
    [SerializeField] SlotShower Slots;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if(LightCharm.activeSelf)
            {
                Slots.RemoveItem("Light Charm");
                LightCharm.GetComponent<LightCharmAI>().Death();
            }


            gameObject.SetActive(false);
        }
    }
}
