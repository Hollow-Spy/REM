using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityDrawer : MonoBehaviour
{
    [SerializeField] GameObject DrawOpenSound, DrawCloseSound;
    bool isBusy,isOpen;
    Animator animator;
    public bool ChosenOne;
    [SerializeField] DrawerAttention AttentionDetector;
    private void Start()
    {
        animator = GetComponentInParent<Animator>();
        
    }
    public void Interaction()
    {
        AttentionDetector.PlayerGotIt = true;
        if (!isBusy)
        {
            isOpen = !isOpen;
            isBusy = true;
            if(isOpen)
            {
                animator.Play("Open");
                Instantiate(DrawOpenSound, transform.position, Quaternion.identity);
                if(ChosenOne)
                {
                  
                    GetComponent<Collider>().enabled = false;
                }
            }
            else
            {
                Instantiate(DrawCloseSound, transform.position, Quaternion.identity);

                animator.Play("Close");

            }

        }
    }

    public void BusyON()
    {
        isBusy = true;
    }
    public void BusyOFF()
    {
        isBusy = false;

    }
}
