using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotDoorTrigger : MonoBehaviour
{
    [SerializeField] BotFuzzy BotAI;
    [SerializeField] DoorOpener door;
    
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Bot") && !door.isOpen)
        {
            if(door.isBusy)
            {
               if(BotAI.State == "Chase")
                {
                    BotAI.HitDoor();
                }
            }
            else
            {
                if (BotAI.State == "Chase")
                {
                   if(door.isBlocked)
                    {
                        BotAI.HitDoor();
                    }
                   else
                    {
                       if(!BotAI.HasPossibleSight())
                        {
                            door.Interact();
                            BotAI.TryingToOpenDoor();
                        }  
                    }
                }

                if (BotAI.State == "Alert" || BotAI.State == "Patrol" && !door.isBlocked)
                {
                    //not force
                    door.Interact();
                    BotAI.TryingToOpenDoor();
                    door.BotOpenDelayFunc();
                }
              
            }
           
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("Bot") && door.isOpen)
        {
            if (door.isBusy)
            {
                //force
            }
            else
            {
                if (!door.BotOpenDelay && BotAI.State == "Chase" || (BotAI.State == "Patrol" && BotAI._PCDelay > 0))
                {
                    //not force
                    door.Interact();
                   
                }

            }

        }
    }
}
