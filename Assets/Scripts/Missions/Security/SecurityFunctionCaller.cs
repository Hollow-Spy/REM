using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityFunctionCaller : MonoBehaviour
{
    [SerializeField] SecurityManager manager;
   public void CallFunc(int num)
    {
        switch(num)
        {
            case 0:
                manager.EnablePrompt();
                break;
            case 1:
                manager.EnableOkayButton();
                break;
         
           
        }
    }
}
