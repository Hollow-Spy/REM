using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuOptionsTick : MonoBehaviour
{

    [SerializeField] int OptionNumber;
    [SerializeField] MenuTranssition transition;
    [SerializeField] GameObject[] Ticks;

        public void TickCheck()
      {
        bool state = transition.GetOptionState(OptionNumber);
        for (int i = 0; i < Ticks.Length; i++)
        {
            Ticks[i].SetActive(state);
        }
     }

    private void Start()
    {
        TickCheck();
    }


}
