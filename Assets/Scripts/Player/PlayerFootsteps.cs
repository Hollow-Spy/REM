using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootsteps : MonoBehaviour
{
    [System.Serializable]
    public class FootstepList
    {
        [SerializeField]
        public GameObject[] Sounds;

        // optionally some other fields
    }


    [SerializeField] Transform Target;
    [SerializeField] LayerMask FloorMask;
    [SerializeField] GameObject[] CurrentSteps;
    [SerializeField] FootstepList[] FootstepSounds;


    [SerializeField] float TimeForStep=.2f,TimeForSprintStep=.1f;
    public float SprintTapIncrement=0;

 
    float CurrentTime,TimeReach;

    PlayerMovement playermov;

  

    private void Start()
    {
        CurrentTime = TimeForStep;
        playermov = GetComponent<PlayerMovement>();
    }
    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if(Physics.Raycast(Target.position,Vector3.down,out hit,3,FloorMask))
        {
           switch(hit.transform.gameObject.tag)
            {
                case "RoomFloor":
                    CurrentSteps = FootstepSounds[0].Sounds;
                    break;
                case "MetalFloor":
                    CurrentSteps = FootstepSounds[1].Sounds;

                    break;
                default:
                    CurrentSteps = FootstepSounds[0].Sounds;

                    break;
            }
        }
      
       if(playermov.isWalking)
        {
            if (playermov.isSprinting)
            {

                TimeReach = TimeForSprintStep - (playermov.SpeedTap * 0.05f);
            }
            else
            {
                TimeReach = TimeForStep;
            }

            CurrentTime -= Time.deltaTime;

            if(CurrentTime < 0)
            {
                CurrentTime = TimeReach;
                Instantiate(CurrentSteps[Random.Range(0, CurrentSteps.Length)], transform.position, Quaternion.identity);
            }


        }

   

    }
}
