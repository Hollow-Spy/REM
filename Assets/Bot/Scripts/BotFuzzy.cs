using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;


public class BotFuzzy : MonoBehaviour
{
    [Header("Overall Settings")]

    [SerializeField] Transform BotTransform;
    [SerializeField] LayerMask GroundLayer;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] float ListenRange;
    [SerializeField] string State;
    [SerializeField] Animator animator;
    [SerializeField] Transform PlayerPos;
    [SerializeField] SkinnedMeshRenderer Meshrenderer;
    [SerializeField] BotFOV botfov;
    [SerializeField] LayerMask PlayerMask;
    [SerializeField] LayerMask ObstacleMask;
  [Header("Aesthetics Settings")]
    [SerializeField] GameObject RedBeamLight, GreenBeamLight,AlertSound;
    [SerializeField] Material EyeMat;
    [SerializeField] GlitchScreenController ScreenGlitcher;
    [SerializeField] float ScreenGlitchRadius;
    [SerializeField] AudioSource ScanAudiosource;
    [SerializeField] GameObject PunchLandSound,SwingPunchSound;
    [Header("Patrol Settings")]
    [SerializeField] Transform[] PatrolPoint;
    [SerializeField] float PatrolRadius;
    IEnumerator PatrolCoroutine;

    [Header("Movement Settings")]
    [SerializeField] float MoveSpeed; //sprint mode needs to be added
    [SerializeField] float Acceleration;
    float MoveSpeedBeforeStop;
    [Header("Movement Detection Settings")]
    [SerializeField] float GraceTime;
    float GraceT;
    Vector3 LastMovement;
    bool FirstMovementCheck = true;
    Quaternion LastRotation;
    IEnumerator AlertCoroutine;
    [Header("Chase Settings")]
    IEnumerator ChaseCoroutine,StaminaUpCoroutine,StaminaDownCoroutine;
    [SerializeField] float MaxChaseTime;
    [SerializeField] float MaxStamina;
    [SerializeField] float SprintMultiplier;
    [SerializeField]
    bool isSprinting;
    float Stamina;
    [SerializeField] AudioSource ChaseAudioSource;
    [SerializeField] Volume ChaseVolume;
    float AfterChaseDelayValue = 3;
    float AfterChaseDelay;
    [Header("Attack Settings")]
    [SerializeField] float AttackRadius;
    [SerializeField] float AttackDelayValue;
    float AttackDelay;
    [SerializeField] Transform AttackPosition;
  
  
    private void Start()
    {
        Stamina = MaxStamina;
        MoveSpeedBeforeStop = MoveSpeed;
        agent.speed = MoveSpeed;
        PlayerPos = GameObject.FindGameObjectWithTag("Player").transform;
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
        float _Speed = MoveSpeed;
        float _accel = Acceleration;
        if (isSprinting)
        {
            _Speed = MoveSpeed * SprintMultiplier;
            _accel = _accel * SprintMultiplier;
        }
        agent.acceleration = _accel;
        agent.speed = _Speed;
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

    void StartSprinting(bool state)
    {
        if(state == isSprinting)
        {
            return;
        }
        if(state && Stamina > MaxStamina * .3f)
        {
            isSprinting = true;

            StartCoroutine(StaminaDownNumerator());

        }
        else
        {
            StartCoroutine(StaminaUpNumerator());
            isSprinting = false;
        }


    }

    IEnumerator StaminaUpNumerator()
    {
        animator.SetFloat("Speed", 1);
        while (!isSprinting && Stamina < MaxStamina)
        {
            yield return null;
            Stamina += 4 * Time.deltaTime;
        }
      
    }
    IEnumerator StaminaDownNumerator()
    {
        animator.SetFloat("Speed", SprintMultiplier * .9f);
        while (isSprinting && Stamina > 0)
        {
            yield return null;
            Stamina -= Time.deltaTime;
        }
        if(Stamina<=0)
        {
            animator.SetFloat("Speed", 1);
            isSprinting = false;
        }
      
    }
   

    public void MovementDetected(Vector3 Pos, Quaternion Rot)
    {
        if (State == "Chase" || AfterChaseDelay > 0)
        {
            LastMovement = Pos;
            LastRotation = Rot;
            return;
        }


        if(State=="Alert")
        {
            if (Pos != LastMovement || Rot != LastRotation)
            {
                LastMovement = Pos;
                LastRotation = Rot;
                GraceT -= Time.deltaTime;
            }

        }
        else
        {
            if (State != "Patrol")
            {
                return;
            }
            else
            {
                if (!FirstMovementCheck && (LastMovement != Pos || LastRotation != Rot))
                {
                    LastMovement = Pos;
                    LastRotation = Rot;
                    SetState("Alert");
                }
                else
                {
                    FirstMovementCheck = false;
                    LastMovement = Pos;
                    LastRotation = Rot;
                    Invoke("ResetFirstMovementCheck", 5);
                }
              
            }
        }
    }

    void ResetFirstMovementCheck()
    {
        FirstMovementCheck = true;
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

   void SetRedMode(bool state)
    {
        if(state)
        {
            GreenBeamLight.SetActive(false);
            RedBeamLight.SetActive(true);
            EyeMat.SetColor("_Emission", Color.red * 3);
        }
        else
        {
            GreenBeamLight.SetActive(true);
            RedBeamLight.SetActive(false);
            EyeMat.SetColor("_Emission", Color.green * 3);
        }
    }

IEnumerator AlertNumerator()
    {
        GraceT = 0;
        StopSpeed();
        animator.Play("Stand");
        Instantiate(AlertSound, transform.position, Quaternion.identity);
        SetRedMode(true);

        AudioClip clip;
        clip = ScanAudiosource.clip;
        ScanAudiosource.volume = 1;
        int randomStartTime = Random.Range(0, clip.samples - 1); //clip.samples is the lengh of the clip in samples
        ScanAudiosource.timeSamples = randomStartTime; //https://forum.unity.com/threads/how-to-make-game-soundtrack-start-at-random-intervals.416947/
        ScanAudiosource.Play();


        float ShockTime = 1;
        float LastGraceT=0;
        while(ShockTime > 0)
        {
           // Debug.Log(ShockTime);
            if (LastGraceT != GraceT)
            {
                float diff = Mathf.Abs(GraceT) - LastGraceT;
                ShockTime -= diff;
                LastGraceT = Mathf.Abs(GraceT);
            }

            ShockTime -= Time.deltaTime;
            yield return null;
        }
       
        EnableSpeed();
        // yield return new WaitForSeconds(1); // add change to move faster 
       // Debug.Log(GraceT * -1.12f);
        AgentMove(CheckPointOfInterest(LastMovement, GraceT * -1.12f, false)); // the bigger more he moved before the grace time reset the bigger the listen radius
        GraceT += GraceTime;  //reset grace time, so if moving during alert will trigger a chase
        if (GraceT < 0)
        {
            StartCoroutine(DecreaseScanAudioNumerator());
            SetState("Chase");
        }
        animator.SetBool("PatrolCheck", false);
        animator.Play("Walk");
     
        yield return new WaitForSeconds(.1f);
        while (GraceT > 0 && agent.remainingDistance > 0.2f && agent.pathStatus == NavMeshPathStatus.PathComplete && agent.pathStatus != NavMeshPathStatus.PathInvalid)
        {
            yield return null;
          
        }


        if(GraceT > 0)
        {
            if (GraceT < GraceTime * .6f)
            {
                animator.Play("Stand");
              
            }
            else
            {
                animator.Play("LookAround");
            }

            yield return new WaitForSeconds(.1f);
            while (animator.GetBool("PatrolCheck") == true && GraceT > 0)
            {
                yield return null;
            }

        }

        // Debug.Log(GraceT);
        StartCoroutine(DecreaseScanAudioNumerator());
        if(GraceT <= 0)
        {
            SetState("Chase");
        }
        else
        {
            SetRedMode(false);
            SetState("Patrol");
        }

    }

    IEnumerator DecreaseScanAudioNumerator()
    {
        while(ScanAudiosource.volume > 0)
        {
            yield return null;
            ScanAudiosource.volume -= Time.deltaTime / 2;
        }
        ScanAudiosource.volume = 0;
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

    IEnumerator ChanceSprintNumerator()
    {
        while(State == "Chase")
        {
            yield return new WaitForSeconds(Random.Range(1.0f, 2.0f));
            float chance = ChanceOnSprint(LastMovement);
            float Hit = Random.Range(0.0f, 1f);
            if (Hit <= chance)
            {
                StartSprinting(true);
            }
            else
            {
                StartSprinting(false);
            }
        }
    }

    IEnumerator IncreaseChaseVolume()
    {
        while(ChaseVolume.weight < 1)
        {
            ChaseVolume.weight += Time.deltaTime;
            yield return null;
        }
    }

   

    IEnumerator ChaseNumerator()
    {
        SetRedMode(true);
        float TimeChasing=MaxChaseTime;

        animator.Play("Walk");
        animator.SetBool("PatrolCheck", false);

        ChaseAudioSource.volume = 1;
        ChaseAudioSource.Play();
        StartCoroutine(IncreaseChaseVolume());
        StartCoroutine(ChanceSprintNumerator());
        while(State == "Chase" && TimeChasing > 0)
        {
            yield return null;
            AgentMove(PlayerPos.position);
            CheckAttackRange();
            RotateTowardsPlayer();
            if (botfov.canSeePlayer)
            {
             
            }
            else
            {
                TimeChasing -= Time.deltaTime;
            }

        }
        AfterChaseDelay = AfterChaseDelayValue;
        Invoke("ResetAfterChase", AfterChaseDelay);
        StartCoroutine(ChaseMusicVolumeReduce());
        SetRedMode(false);
        SetState("Patrol");
    }

    void RotateTowardsPlayer()
    {
        if(Vector3.Distance(BotTransform.position,PlayerPos.position) <= ScreenGlitchRadius)
        {
          
            Vector3 targetDirection = PlayerPos.position - BotTransform.position;
             Vector3 rot = Vector3.RotateTowards(BotTransform.forward, targetDirection, 10*Time.deltaTime,0.0f );
            BotTransform.rotation = Quaternion.LookRotation(rot);
        }
    }

    void CheckAttackRange()
    {
        if(AttackDelay > 0)
        {
            return;
        }

        if (PlayerInAttackRadius())
        {
          
            Instantiate(SwingPunchSound, BotTransform.position, Quaternion.identity);
            AttackDelay = AttackDelayValue;
            Invoke("ResetAttackDelay", AttackDelay);
            animator.Play("Attack");
            Invoke("StopSpeed", 0.06f); 
        }
        else
        {
          
        }
        
    }

    bool PlayerInAttackRadius()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(AttackPosition.position, AttackRadius, PlayerMask);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - BotTransform.position).normalized;
            float distanceToTarget = Vector3.Distance(transform.position, target.position);

            if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, ObstacleMask))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }
    
    public void AttackLandCheck()
    {

       if(PlayerInAttackRadius())
        {
         
            Instantiate(PunchLandSound, BotTransform.position, Quaternion.identity);

        }

    }


    void ResetAttackDelay()
    {
        AttackDelay = 0;
    }


    void ResetAfterChase()
    {
        AfterChaseDelay = 0;
    }

    float ChanceOnSprint(Vector3 Pos)
    {
        float Chance = 0;

        if(Vector3.Distance(BotTransform.position, Pos) < ScreenGlitchRadius * 1.2f )
        {
            Chance += .2f;
        }
        if(botfov.canSeePlayer)
        {
            Chance += .3f;
        }
        if(Stamina > MaxStamina / 2)
        {
            Chance += .25f;
        }
        if(!FirstMovementCheck)
        {
            Chance += .25f;
        }

        return Chance;
    }

    IEnumerator ChaseMusicVolumeReduce()
    {
        yield return new WaitForSeconds(.1f);
        while(State!="Chase" && ChaseAudioSource.volume > 0)
        {
            yield return null;
            ChaseAudioSource.volume -= Time.deltaTime / 3;
            ChaseVolume.weight -= Time.deltaTime / 3;
        }
        if(State != "Chase")
        {
            ChaseAudioSource.volume = 0;
            ChaseVolume.weight = 0;
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
            case "Alert":
                if (AlertCoroutine != null)
                {
                    StopCoroutine(AlertCoroutine);
                }
                break;
            case "Chase":
                if (ChaseCoroutine != null)
                {
                    StopCoroutine(ChaseCoroutine);
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
            case "Alert":
                State = newstate;
                AlertCoroutine = AlertNumerator();
                StartCoroutine(AlertCoroutine);
                break;
            case "Chase":
                State = newstate;
                ChaseCoroutine = ChaseNumerator();
                StartCoroutine(ChaseCoroutine);
                break;
        }
    }

    private void OnDisable()
    {
        EyeMat.SetColor("_Emission", Color.green * 3);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            SetState("Patrol");
        }
        float distance = Vector3.Distance(BotTransform.position, PlayerPos.position);

     
        if (distance < ScreenGlitchRadius )
        {
            float NoiseAmount = 100 - (distance / ScreenGlitchRadius) * 100;
            float glitchpower = 50 - (distance / ScreenGlitchRadius) * 50;
            float scanlines = 1 - (distance / ScreenGlitchRadius);

            ScreenGlitcher.noiseAmount = NoiseAmount;
            ScreenGlitcher.glitchPower = glitchpower;
            ScreenGlitcher.scanLinesPower = scanlines;
        }
        else
        {
            ScreenGlitcher.noiseAmount = 0;
            ScreenGlitcher.glitchPower = 0;
            ScreenGlitcher.scanLinesPower = 0;
        }
     
    }
}
