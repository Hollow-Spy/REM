using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandoGoalComplete : MonoBehaviour
{
    [SerializeField] int NewGoal;
    [SerializeField] GameObject NextMission;
    [SerializeField] Transform CheckPointPos;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            FindObjectOfType<GoalManager>().UpdateGoal(NewGoal);
            if(NextMission)
            {
                NextMission.SetActive(true);
            }
            CheckpointManager checkpoint = GameObject.FindGameObjectWithTag("Checkpoint").GetComponent<CheckpointManager>();
            checkpoint.CheckPointPos = CheckPointPos.position;
            checkpoint.CurrentMission = 2;
            gameObject.SetActive(false);
        }
    }
}
