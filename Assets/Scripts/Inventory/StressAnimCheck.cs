using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StressAnimCheck : MonoBehaviour
{
    [SerializeField] PlayerHealth playerhealth;
    [SerializeField] Animator Stressanimator;

    private void Update()
    {
        Stressanimator.SetFloat("Speed", 4 - playerhealth.Health);
    }
   
}
