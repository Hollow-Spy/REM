using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisengageButton : MonoBehaviour
{
    [SerializeField] GameObject Alert;
    public void Interaction()
    {
       gameObject.layer = 0;
        Alert.SetActive(true);
        
    }
}
