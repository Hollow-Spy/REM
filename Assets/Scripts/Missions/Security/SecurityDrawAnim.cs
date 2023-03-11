using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityDrawAnim : MonoBehaviour
{
     SecurityDrawer drawer;
    private void Start()
    {
        drawer = GetComponentInChildren<SecurityDrawer>();
    }

    public void busyON()
    {
        drawer.BusyON();
    }
    public void busyOFF()
    {
        drawer.BusyOFF();
    }
}
