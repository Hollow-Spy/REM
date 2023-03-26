using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpener : MonoBehaviour
{

    public Animator dooranim;
    public GameObject sound,sound2;
    public GameObject melody1, melody2,DenySound;

    [SerializeField] GameObject[] OpenIcon, CloseIcon, BlockIcon;

    [SerializeField] GameObject DelaySound;
    // Update is called once per frame

    public bool isBusy, isOpen,isBlocked;

 
    float MultiClick = 0;

   public bool BotOpenDelay;
   
    public bool ForceCloseNBlock()
    {
        if(ForceClose())
        {
            for (int i = 0; i < OpenIcon.Length; i++)
            {
                OpenIcon[i].SetActive(false);
                CloseIcon[i].SetActive(false);
                BlockIcon[i].SetActive(true);
            }
           
            isBlocked = true;

            return true;

        }
        else
        {
            return false;
        }
    }

    public void Unlock()
    {
        isBlocked = false;
        for (int i = 0; i < OpenIcon.Length; i++)
        {
            OpenIcon[i].SetActive(true);
            CloseIcon[i].SetActive(false);
            BlockIcon[i].SetActive(false);
        }
    }

    public bool IsBlocked()
    {
        return BlockIcon[0];
    }
    public void Interact()
    {
        if(!isBusy)
        {
           if(isBlocked)
            {
                for (int i = 0; i < OpenIcon.Length; i++)
                {
                    BlockIcon[i].GetComponent<Animator>().Play("ButtonFlash", - 1,  0.0f); 

                }
                Instantiate(DenySound, transform.position, Quaternion.identity);
                Debug.Log("g");
                return;
            }
           if(BotOpenDelay)
            {
                Instantiate(DelaySound, transform.position, Quaternion.identity);
                return;
            }

            isOpen = !isOpen;
            if (isOpen)
            {
                OpenDoor();
              
            }
            else
            {
                CloseDoor();
            }
            return;
        }
        else
        {
            MultiClick++;
            if (!isOpen && MultiClick == 3)
            {
                TemporaryBlock();
            }
        }

       
    }

    void TemporaryBlock()
    {
        for (int i = 0; i < OpenIcon.Length; i++)
        {
            OpenIcon[i].SetActive(false);
            CloseIcon[i].SetActive(false);
            BlockIcon[i].SetActive(true);
        }
        isBlocked = true;

        Invoke("Unlock", 10);
        Invoke("ResetLockDelay", 30);
    }


    void ResetLockDelay()
    {
        MultiClick = 0;
    }

    public bool ForceOpen()
    {
        if (!isBusy)
        {
            if (!isOpen)
            {
                OpenDoor();
                isOpen = true;


            }
            else
            {
                return true;
            }
        }
        return false;
    }

    public bool ForceClose()
    {
        if(!isBusy )
        {
           
            if (isOpen)
            {
                isOpen = false;
                CloseDoor();

            }
            else
            {
                return true;
            }
        }
        return false;
    }

   public void BotOpenDelayFunc()
    {
        if(!BotOpenDelay)
        {
            BotOpenDelay = true;
            Invoke("ResetBotDelay",5);
        }
      
    }
    void ResetBotDelay()
    {
        BotOpenDelay = false;

   

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
