using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallDoorsExchange : MonoBehaviour
{
    bool Inside,isBusy;
    [SerializeField] DoorOpener DoorWillClose, DoorWillOpen;
    [SerializeField] ParticleSystem[] Smoke;
    [SerializeField] GameObject SmokeSound;

    private void OnEnable()
    {
        Inside = false;
        isBusy = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Inside = true;
        }
        
    }

    private void Update()
    {
        if(Inside && !isBusy)
        {
           // DoorWillClose.ForceClose();
            //DoorWillOpen.ForceClose();
           
            if (DoorWillOpen.ForceClose() && DoorWillClose.ForceClose()  )
            {
                isBusy = true;
                for(int i=0;i<Smoke.Length;i++)
                {
                    Smoke[i].Play();
                }
                Instantiate(SmokeSound, transform.position, Quaternion.identity);
                StartCoroutine(ExchangingDoors());
            }
        }
        
    }
    
    IEnumerator ExchangingDoors()
    {
        //Smoke.Play();
        yield return new WaitForSeconds(3);
        while( !DoorWillOpen.ForceOpen() )
        {
            yield return null;
        }
        
        gameObject.SetActive(false);

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Inside = false;
        }
    }
}
