using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommsPingButton : MonoBehaviour
{
    [SerializeField] CommsManager manager;
    [SerializeField] GameObject PingingText,Menu;
  public void Interaction()
    {
        manager.Pinging();
        PingingText.SetActive(true);
        Menu.SetActive(false);
    }
}
