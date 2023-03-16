using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandoGoalComplete : MonoBehaviour
{
    [SerializeField] int NewGoal;
    [SerializeField] GameObject NextMission;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            FindObjectOfType<GoalManager>().UpdateGoal(NewGoal);
            if(NextMission)
            {
                NextMission.SetActive(true);
            }
            gameObject.SetActive(false);
        }
    }
}
