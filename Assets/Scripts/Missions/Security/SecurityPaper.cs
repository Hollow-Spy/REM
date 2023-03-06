using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityPaper : MonoBehaviour
{
    [SerializeField] Transform[] Positions;
    [SerializeField] Transform[] Drawers;
    [SerializeField] GameObject PaperSound,ConfirmationInteract;
  
    void Start()
    {
        int rand = Random.Range(0, Positions.Length);
        transform.position = Positions[rand].position;
        transform.parent = Drawers[rand];
    }

    public void Interaction()
    {
        Instantiate(PaperSound, transform.position, Quaternion.identity);
        ConfirmationInteract.SetActive(true);
        //add to inventory (future) 
        gameObject.SetActive(false);
    }
  
}
