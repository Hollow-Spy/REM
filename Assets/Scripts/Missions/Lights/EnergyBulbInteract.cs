using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBulbInteract : MonoBehaviour
{
    [SerializeField] EnergyManager manager;
    [SerializeField] GameObject ActivateSound;
    [SerializeField] SpriteRenderer sprite;
    
    public void Interaction()
    {
        sprite.color = Color.green;
        Instantiate(ActivateSound, transform.position, Quaternion.identity);
        gameObject.layer = 0;
        manager.BulbClick();

    }
}
