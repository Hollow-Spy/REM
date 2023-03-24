using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int Health=3;
    [SerializeField] Animator animator;
    IEnumerator ReducingLayerWeightCoroutine;
    public void HurtPlayer()
    {

        Health--;
        if (ReducingLayerWeightCoroutine != null)
        {
            StopCoroutine(ReducingLayerWeightCoroutine);
        }
        animator.SetLayerWeight(animator.GetLayerIndex("HurtLayer") , 1);
        animator.Play("PlayerHurt");

        if (Health > 1)
        {
            animator.SetBool("HealthCritical", false);
            ReducingLayerWeightCoroutine = ReduceLayerWeightNumerator();
            StartCoroutine(ReducingLayerWeightCoroutine); 
        }
        else
        {
            animator.SetBool("HealthCritical", true);
        }
        
    }

    IEnumerator ReduceLayerWeightNumerator()
    {
        yield return new WaitForSeconds(1);
        while(animator.GetLayerWeight(animator.GetLayerIndex("HurtLayer")) > 0)
        {
            float weight = animator.GetLayerWeight(animator.GetLayerIndex("HurtLayer"));
            animator.SetLayerWeight(animator.GetLayerIndex("HurtLayer"),  weight - Time.deltaTime);
            yield return null;
        }

    }

}
