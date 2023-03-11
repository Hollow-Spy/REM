using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityPaper : MonoBehaviour
{
    [SerializeField] Transform[] Positions;
    [SerializeField] Transform[] Drawers;
   
    [SerializeField] SlotShower Slots;


  
    void Start()
    {
        int rand = Random.Range(0, Positions.Length);
        transform.position = Positions[rand].position;
        transform.parent = Drawers[rand];
        Drawers[rand].GetComponent<SecurityDrawer>().ChosenOne = true;
    }

   
  
}
