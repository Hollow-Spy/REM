using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandoGoalComplete : MonoBehaviour
{
    [SerializeField] int NewGoal;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            FindObjectOfType<GoalManager>().UpdateGoal(NewGoal);
            gameObject.SetActive(false);
        }
    }
}
