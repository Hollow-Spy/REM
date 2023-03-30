using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuOptions : MonoBehaviour
{
    [SerializeField] MenuTranssition transition;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            transition.OptionsClicked();

        }
    }
}
