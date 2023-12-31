using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionsManager : MonoBehaviour
{

    [SerializeField] GameObject[] Missions;
    [SerializeField] GameObject Bot;
    [SerializeField] GoalManager goalmanager;
    [SerializeField] GameObject Mission5Trigger;
    

   

    void Start()
    {
        CheckpointManager checkpoint = GameObject.FindGameObjectWithTag("Checkpoint").GetComponent<CheckpointManager>();
        switch (checkpoint.CurrentMission)
        {
            case 0:
                Missions[0].SetActive(true);
                Missions[1].SetActive(true);
                break;
            case 1:
                Missions[1].SetActive(true);
                goalmanager.UpdateGoal(1);
                
                break;
            case 2:
                Missions[2].SetActive(true);
                goalmanager.UpdateGoal(2);
                
                break;

            case 3:
                Missions[3].SetActive(true);
                goalmanager.UpdateGoal(3);
                Bot.SetActive(true);
              
                break;
            case 4:
                Missions[4].SetActive(true);
                Bot.SetActive(true);
                goalmanager.UpdateGoal(4);
                
                break;
            case 5:
                Mission5Trigger.SetActive(true);
                Bot.SetActive(true);
                FindObjectOfType<BotFuzzy>().SetFuzzy();
                goalmanager.UpdateGoal(5);
                break;

        }

        IntercomInteract.ObstacleSpeed = 0.5f;
        IntercomInteract.ObstacleRotationSpeed = 1f;
        IntercomInteract.MaxSpawnTime = 3f;
        IntercomInteract.MinSpawnTime = 1;


    }



}
