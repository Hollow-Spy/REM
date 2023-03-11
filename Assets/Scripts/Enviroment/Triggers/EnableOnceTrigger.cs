using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableOnceTrigger : MonoBehaviour
{
    [SerializeField] GameObject[] Objects;
    [SerializeField] bool[] Enable;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            for(int i=0;i<Objects.Length;i++)
            {
                Objects[i].SetActive(Enable[i]);
            }
            Destroy(gameObject);
        }
    }
}
