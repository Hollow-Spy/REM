using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisengageButton : MonoBehaviour
{
    [SerializeField] GameObject Alert;
    public void Interaction()
    {
       gameObject.tag = "Untagged";
        Alert.SetActive(true);
        
    }
}
