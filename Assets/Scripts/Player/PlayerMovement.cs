using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Animator animator;
    CharacterController charController;
    [SerializeField] float MovementSpeed=.25f;
    [SerializeField] float RotationSpeed=200;
    [SerializeField] float SprintMultiplier=2;
    [SerializeField] float Gravity=-9.81f,GroundDistance=.4f;
    [SerializeField] Transform GroundCheck;
    [SerializeField] LayerMask GroundMask;
    Vector3 currentVelocity;

    bool StopMovement;
    [SerializeField] Transform botTransform;
    bool isGrounded;
    public bool isSprinting,isWalking;

    float InitialYPos;

    void Start()
    {
        Time.timeScale = 1;
        InitialYPos = transform.position.y;
     
        Vector3 SavePoint = GameObject.FindGameObjectWithTag("Checkpoint").GetComponent<CheckpointManager>().CheckPointPos;
        if (SavePoint != Vector3.zero)
        {
            transform.position = new Vector3(SavePoint.x, InitialYPos, SavePoint.z);
        }
      
        animator = GetComponent<Animator>();
        charController = GetComponent<CharacterController>();
    }

    public void ShoveEnd()
    {
        StopMovement = false;
        StartCoroutine(ReduceShoveAnimWeight());
    }
    IEnumerator ReduceShoveAnimWeight()
    {
        while (animator.GetLayerWeight(animator.GetLayerIndex("ActionLayer")) > 0)
        {
            yield return null;
            float value = animator.GetLayerWeight(animator.GetLayerIndex("ActionLayer"));
            animator.SetLayerWeight(animator.GetLayerIndex("ActionLayer"), value-=Time.deltaTime);
        }
    }

    IEnumerator RotatingTowardsBotNumerator()
    {
        Vector3 botpos;
        while(StopMovement)
        {    
            yield return null;
            botpos = new Vector3(botTransform.position.x, InitialYPos, botTransform.position.z); 
            Vector3 targetDirection = botpos - transform.position;
            Vector3 rot = Vector3.RotateTowards(transform.forward, -targetDirection, 10 * Time.deltaTime, 0.0f);
            transform.rotation = Quaternion.LookRotation(rot);
           
        }
    }

    public void Shove()
    {
        StopMovement = true;
        transform.LookAt(new Vector3(botTransform.position.x,transform.position.y,botTransform.position.z ));
        animator.Play("Shove");
        animator.SetLayerWeight(animator.GetLayerIndex("ActionLayer"), 1);
        StartCoroutine(RotatingTowardsBotNumerator());

    }
    public void ThrownOff()
    {
        StopMovement = true;
        animator.Play("Fall");
    }
    public void BackUp()
    {
        
        StopMovement = false;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(GroundCheck.position,GroundDistance,GroundMask);

        if (isGrounded && currentVelocity.y < 0)
        {
            currentVelocity.y = -2;
        }

        if(!StopMovement)
        {

       
        Vector3 movedir = new Vector3( Input.GetAxisRaw("Horizontal"),0, Input.GetAxisRaw("Vertical"));

        if(movedir != Vector3.zero)
        {
            transform.Rotate(0, movedir.x * Time.deltaTime * RotationSpeed,0);
          
          
           if(movedir.z > 0)
            {
                float speed = MovementSpeed;
                animator.SetFloat("Speed", 1);

                isWalking = true;

                if (Input.GetKey(KeyCode.LeftShift))
                {
                    isSprinting = true;
                    float SprintSpeed = SprintMultiplier;
                    animator.SetFloat("Speed", SprintSpeed - .4f);
                    speed *= SprintSpeed;
                }
                else
                {
                    isSprinting = false;
                }

                charController.Move(-transform.forward * speed  * Time.deltaTime);
            }
           else
            {
                isWalking = isSprinting = false;
                animator.SetFloat("Speed", 0);
            }

            //charController.Move(movedir * MovementSpeed * Time.deltaTime);

        }
        else
        {
            animator.SetFloat("Speed", 0);
            isWalking = isSprinting = false;

        }



        currentVelocity.y += Gravity * Time.deltaTime;
        charController.Move(currentVelocity * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, InitialYPos, transform.position.z);

        }
    }
}
