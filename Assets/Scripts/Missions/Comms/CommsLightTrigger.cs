using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommsLightTrigger : MonoBehaviour
{
    [SerializeField] GameObject CommsMission,LightsMission;
    [SerializeField] GoalManager goal;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //goal.UpdateGoal(3);
            LightsMission.SetActive(true);
            CommsMission.SetActive(false);
        }
    }
}
