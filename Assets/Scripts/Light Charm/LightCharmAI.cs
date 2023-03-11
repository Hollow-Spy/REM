using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightCharmAI : MonoBehaviour
{
    [SerializeField] Transform ShoulderPos;
    [SerializeField] float Speed,RotSpeed;
    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, ShoulderPos.position, Speed * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, ShoulderPos.rotation, RotSpeed * Time.deltaTime);
    }
}
