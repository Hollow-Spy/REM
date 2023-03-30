using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuOptionsButton : MonoBehaviour
{
    [SerializeField] MenuTranssition transition;
    [SerializeField] int FuncNum;

    [SerializeField] MenuOptionsTick Ticker;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            transition.OptionButtonClicked(FuncNum);
            
            if(Ticker)
            {
                Ticker.TickCheck();
              
            }
        }
    }
  
}
