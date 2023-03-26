using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransmitterManager : MonoBehaviour
{
    [SerializeField] GameObject[] TransmittorsTriggers;
    int NecessaryTransmittors;
    int ActiveTransmittors=0;
    [SerializeField] GoalManager goalmanager;
    [SerializeField] GameObject LastMissionTrigger;
    [SerializeField] Transform CheckpointPos;
    private void Start()
    {
        NecessaryTransmittors = TransmittorsTriggers.Length;
         TransmittorsTriggers[0].SetActive(true);
        CheckpointManager checkpoint = GameObject.FindGameObjectWithTag("Checkpoint").GetComponent<CheckpointManager>();
        checkpoint.CheckPointPos = CheckpointPos.position;
        checkpoint.CurrentMission = 4;
    }

    public void AddTransmittor()
    {
        ActiveTransmittors++;
        
        if (ActiveTransmittors == NecessaryTransmittors)
        {
            goalmanager.UpdateGoal(5);
            LastMissionTrigger.SetActive(true);
        }
        else
        {
            TransmittorsTriggers[ActiveTransmittors].SetActive(true);
        }
    }
}
