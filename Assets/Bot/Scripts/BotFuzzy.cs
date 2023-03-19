using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BotFuzzy : MonoBehaviour
{
    [Header("Overall Settings")]

    [SerializeField] Transform BotTransform;
    [SerializeField] LayerMask GroundLayer;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] float ListenRange;
    [SerializeField] string State;
    [SerializeField] Animator animator;

    [Header("Patrol Settings")]

    [SerializeField] Transform[] PatrolPoint;
    [SerializeField] float PatrolRadius;
    IEnumerator PatrolCoroutine;

    [Header("Movement Settings")]
    [SerializeField] float MoveSpeed;
    float MoveSpeedBeforeStop;
    //sprint mode
    private void Start()
    {
        MoveSpeedBeforeStop = MoveSpeed;
        agent.speed = MoveSpeed;
    }

    void AgentMove(Vector3 destination)
    {
        if(destination != Vector3.zero)
        {
            agent.SetDestination(destination);
        }
    }
    public void EnableSpeed()
    {
        MoveSpeed = MoveSpeedBeforeStop;
        agent.speed = MoveSpeed;
    }
    public void StopSpeed()
    {
        if(MoveSpeed > 0)
        {
            MoveSpeedBeforeStop = MoveSpeed;
        }
        MoveSpeed = 0;
        agent.speed = MoveSpeed;
    }

    public void TryingToOpenDoor()
    {
        if(State == "Patrol")
        {
            animator.Play("Stand");
            StopSpeed();
            Invoke("EnableSpeed",2);
        }
    }

    Vector3 CheckPointOfInterest(Vector3 pos, float radius, bool ignoreDistance)
    {

        if(Physics.CheckSphere(pos,radius, GroundLayer))
        {
            for(int i=0;i<40;i++)
            {
                //in this function we'll be given a point and try to select a random pos within that radius, if its within the listen range we'll return it
                float distance = Vector3.Distance(pos, BotTransform.position); //we get distance
                float maxdistance = ListenRange + radius;
                float Reduction = distance / maxdistance;
               
                Vector3 point = Random.insideUnitSphere.normalized * (radius*Reduction) + pos  ;        //where we check in radius
              
                point = new Vector3(point.x, PatrolPoint[0].position.y, point.z);
                
               
                if (Physics.CheckSphere(point,0.3f,GroundLayer) && (distance < maxdistance || ignoreDistance) )
                {
                    return point;
                }
              
            }
            pos = new Vector3(pos.x, PatrolPoint[0].position.y, pos.z); //here we try to use the default start pos, if its not in range just return zero
            if(Vector3.Distance(pos, BotTransform.position) < ListenRange)
            {
                return pos;
            }
        }
        return Vector3.zero;
    }


    IEnumerator PatrolNumerator()
    {
        int lastPatrolPoint=-1;
        int randomnum=0;
        while (State=="Patrol")
        {
         
            for(int i=0;i<10;i++)
            {
                randomnum = Random.Range(0, PatrolPoint.Length);
                if(randomnum != lastPatrolPoint)
                {
                    break;
                }
            }
            lastPatrolPoint = randomnum;
            Vector3 randomPos = PatrolPoint[randomnum].position;
            AgentMove( CheckPointOfInterest(randomPos, PatrolRadius,true));

            float initialDistance = Vector3.Distance(randomPos, BotTransform.position);
            float maxdistance = ListenRange + PatrolRadius;
            float Reduction = initialDistance / maxdistance;
          
            yield return new WaitForSeconds(.1f);
            while(agent.remainingDistance > 0 && agent.pathStatus==NavMeshPathStatus.PathComplete && agent.pathStatus != NavMeshPathStatus.PathInvalid)
            {
                yield return null;
            }
            if(Reduction < .5f)
            {
                animator.Play("LookAround");
            }
            else
            {
                animator.Play("Stand");
            }
            yield return new WaitForSeconds(.1f);
            while(animator.GetBool("PatrolCheck") == true)
            {
                yield return null;
            }

        }
    }

    void SetState(string newstate)
    {
        switch(State)
        {
            case "Patrol":
                if (PatrolCoroutine != null)
                {
                    StopCoroutine(PatrolCoroutine);
                }
                break;
        }

        switch(newstate)
        {
            case "Patrol":
                State = newstate;
                PatrolCoroutine = PatrolNumerator();
                StartCoroutine(PatrolCoroutine);
                break;
        }
    }



    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            SetState("Patrol");
        }
    }
}
