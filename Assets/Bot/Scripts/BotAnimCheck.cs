using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotAnimCheck : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] BotFuzzy BotAI;
    [SerializeField] GameObject WalkSound;
   public void StartPatrolCheck()
    {
        animator.SetBool("PatrolCheck", true);
    }
    public void EndPatrolCheck()
    {
        animator.SetBool("PatrolCheck", false);
    }
    public void CanMove()
    {
        BotAI.EnableSpeed();
    }

    public void PlayWalkSound()
    {
        Instantiate(WalkSound, transform.position, Quaternion.identity);
    }
    public void CannotMove()
    {
        BotAI.StopSpeed();

    }
}


