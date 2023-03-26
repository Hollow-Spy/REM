using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoveTap : MonoBehaviour
{
    [SerializeField] MouseInteract MainClickInteract;
    [SerializeField] PlayerMovement playermovement;
    public bool CanShove;

    [SerializeField] BotFuzzy botai;
    [SerializeField] GameObject ShoveSound,MouseIcon;
    void Update()
    {
        if(CanShove && botai.PlayerInAttackRadius() )
        {
            MouseIcon.SetActive(true);

          if(Input.GetMouseButtonDown(0) && MainClickInteract.OnScreen)
            {
                CanShove = false;
                playermovement.Shove();
                Instantiate(ShoveSound, transform.position, Quaternion.identity);
            }

        }
        else
        {
            MouseIcon.SetActive(false);

        }
    }

    public void StaggerBot()
    {
        botai.Stagger();
    }
        

}
