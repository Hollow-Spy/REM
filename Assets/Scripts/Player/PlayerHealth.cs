using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int Health=3;
    [SerializeField] Animator animator,BloodScreenanimator;
    
    [SerializeField] Image nervousSystem;
    IEnumerator ReducingLayerWeightCoroutine;
    [SerializeField] ParticleSystem BloodParticles,BloodDripParticles;
    [SerializeField] Transform botTransform;
    [SerializeField] GameObject[] BloodPuddles;
    [SerializeField] float shake_power, shake_frequency, shake_time;
    public void HurtPlayer()
    {

        Health--;
        if (ReducingLayerWeightCoroutine != null)
        {
            StopCoroutine(ReducingLayerWeightCoroutine);
        }
        animator.SetLayerWeight(animator.GetLayerIndex("HurtLayer") , 1);
        animator.Play("PlayerHurt");
        updateMeters();
        BloodParticles.gameObject.transform.LookAt(new Vector3(botTransform.position.x,BloodParticles.transform.position.y, botTransform.position.z));
        BloodParticles.Play();
        Instantiate(BloodPuddles[Random.Range(0, BloodPuddles.Length)], new Vector3(transform.position.x, 0.0031f, transform.position.z),Quaternion.identity );

        if (Health > 1)
        {
           
            animator.SetBool("HealthCritical", false);
            BloodScreenanimator.Play("BloodSplatter");
            ReducingLayerWeightCoroutine = ReduceLayerWeightNumerator();
            StartCoroutine(ReducingLayerWeightCoroutine); 
        }
        else
        {
         
            BloodScreenanimator.Play("BloodSplatterCritical");
            BloodScreenanimator.SetBool("HealthCritical", true);
            animator.SetBool("HealthCritical", true);
        }
        
        if(Health <= 0)
        {
            //death
        }


    }

    void updateMeters()
    {
     
        switch(Health)
        {
            case 3:
                nervousSystem.color = Color.white;
                break;
            case 2:
                BloodDripParticles.Stop();
                nervousSystem.color = Color.yellow;
                break;
            case 1:
                BloodDripParticles.Play();
                nervousSystem.color = Color.red;
                break;
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
