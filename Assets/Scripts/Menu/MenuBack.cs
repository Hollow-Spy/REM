using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBack : MonoBehaviour
{

    [SerializeField] MenuTranssition transition;
    [SerializeField] GameObject CurrentScreen;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            transition.BackClicked(CurrentScreen);

        }
    }
}
