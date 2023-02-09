using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitchChanger : MonoBehaviour
{
    [SerializeField] float minPitch, maxPitch;
    void Start()
    {
        GetComponent<AudioSource>().pitch = Random.Range(minPitch, maxPitch);
        
    }

   
}
