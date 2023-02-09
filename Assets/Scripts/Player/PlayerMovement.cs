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

    public bool isSprinting,isWalking;
    
    void Start()
    {
        animator = GetComponent<Animator>();
        charController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movedir = new Vector3( Input.GetAxisRaw("Horizontal"),0, Input.GetAxisRaw("Vertical"));

        


        if(movedir != Vector3.zero)
        {
            transform.Rotate(0, movedir.x * Time.deltaTime * RotationSpeed,0);
            //charController.Move(movedir * MovementSpeed * Time.deltaTime);
          //  Vector3 Movement = new Vector3(-movedir.z, 0, 0);
           // Debug.Log(Movement);

          
           if(movedir.z > 0)
            {
                float speed = MovementSpeed;
                animator.SetFloat("Speed", 1);

                isWalking = true;

                if (Input.GetKey(KeyCode.LeftShift))
                {
                    isSprinting = true;
                    animator.SetFloat("Speed", 2);
                    speed *= SprintMultiplier;
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

    }
}
