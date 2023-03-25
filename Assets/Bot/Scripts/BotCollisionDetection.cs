using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotCollisionDetection : MonoBehaviour
{
    [SerializeField] BotFuzzy bot;
 
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            bot.isCollidingPlayer = true;
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            bot.IncreaseBodyBlockTimer();
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            bot.isCollidingPlayer = false;
            bot.ResetBodyBlockTimer();
        }
    }
}
