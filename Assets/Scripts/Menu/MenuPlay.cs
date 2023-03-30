using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPlay : MonoBehaviour
{
    [SerializeField] MenuTranssition transition;
   
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            transition.PlayClicked();
         
        }
    }
}
