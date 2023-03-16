using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuStartGame : MonoBehaviour
{
    [SerializeField] MenuTranssition transition;
    [SerializeField] int mode;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            transition.StartGameClicked(mode);

        }
    }
}
