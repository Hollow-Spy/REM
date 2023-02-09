using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpener : MonoBehaviour
{

    public Animator dooranim;
    public GameObject sound,sound2;
    public GameObject melody1, melody2;

    [SerializeField] GameObject[] OpenIcon, CloseIcon, BlockIcon;
    // Update is called once per frame

    bool isBusy;
    bool isOpen;
    public void Interact()
    {
        if(!isBusy)
        {
            isOpen = !isOpen;
            if (isOpen)
            {
                OpenDoor();
            }
            else
            {
                CloseDoor();
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
    void OpenDoor()
    {
        for(int i=0;i<OpenIcon.Length;i++)
        {
            OpenIcon[i].SetActive(false);
            CloseIcon[i].SetActive(true);
        }
     
            dooranim.Play("DoorOpen");
            Instantiate(sound, transform.position, Quaternion.identity);
            Instantiate(melody1, transform.position, Quaternion.identity);
       
       
    }
     void CloseDoor()
    {
        for (int i = 0; i < OpenIcon.Length; i++)
        {
            OpenIcon[i].SetActive(true);
            CloseIcon[i].SetActive(false);
        }
       


        dooranim.Play("DoorClose");
        Instantiate(sound2, transform.position, Quaternion.identity);
        Instantiate(melody2, transform.position, Quaternion.identity);
    }
   
}
