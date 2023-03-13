using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransmitterObstacle : MonoBehaviour
{
    public float Speed;
    public float RotSpeed;

    int rotdir;
    private void Start()
    {
        rotdir  = Random.Range(0, 2);
        if(rotdir == 0)
        {
            rotdir = -1;
        }
        
    }
    void Update()
    {
        transform.localScale = new Vector3(transform.localScale.x + Speed * Time.deltaTime, transform.localScale.y + Speed * Time.deltaTime);
        transform.Rotate(0, 0, RotSpeed * rotdir * Time.deltaTime);

        if(transform.localScale.x >= 0.7f)
        {
            Destroy(gameObject);
        }
    }
}
