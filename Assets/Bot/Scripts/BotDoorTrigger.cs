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
                //force
            }
            else
            {
                if(BotAI.State == "Patrol")
                {
                    //not force
                    door.Interact();
                    BotAI.TryingToOpenDoor();
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
                if (BotAI.State == "Chase" || (BotAI.State == "Patrol" && BotAI._PCDelay > 0))
                {
                    //not force
                    door.Interact();
                   
                }

            }

        }
    }
}
