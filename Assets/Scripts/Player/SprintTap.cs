using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprintTap : MonoBehaviour
{
    [SerializeField] MouseInteract MainClickInteract;
    [SerializeField] PlayerMovement playermovement;
    [SerializeField] float SpeedDecrease, SpeedGain,SpeedCap;

    // Update is called once per framefe
    void Update()
    {
        if(MainClickInteract.OnScreen && Input.GetMouseButtonDown(0))
        {
            if(playermovement.SpeedTap > SpeedCap)
            {
                playermovement.SpeedTap = SpeedCap;
            }
            else
            {
                playermovement.SpeedTap += SpeedGain;
            }

        }
        if(playermovement.SpeedTap > 0)
        {
            playermovement.SpeedTap -= SpeedDecrease * Time.deltaTime;
        }
        else
        {
            playermovement.SpeedTap = 0;
        }
    }
}
