using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotCollisionDetection : MonoBehaviour
{
    [SerializeField] BotFuzzy bot;
    [SerializeField] float Delay;
    float delayT;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            bot.isCollidingPlayer = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            bot.isCollidingPlayer = false;
        }
    }
}
