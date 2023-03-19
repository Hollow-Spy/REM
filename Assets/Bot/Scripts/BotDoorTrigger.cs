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
                //not force
                door.Interact();
                BotAI.TryingToOpenDoor();
            }
           
        }
    }
}