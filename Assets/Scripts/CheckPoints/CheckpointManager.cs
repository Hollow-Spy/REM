using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    static CheckpointManager instance;

    public int CurrentMission;
    public Vector3 CheckPointPos;

 


    private void Awake()
    {
       if(instance==null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }

    }
}
