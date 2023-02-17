using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotationFollow : MonoBehaviour
{
    [SerializeField] Transform PlayerTransform;
    [SerializeField] float Damp;
    float lookRotY;
    private void Awake()
    {
        lookRotY = transform.rotation.y;
    }
    private void OnEnable()
    {
        var lookPos = PlayerTransform.position - transform.position;
        lookPos.y = lookRotY;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = rotation;

    }

    void Update()
    {

        var lookPos = PlayerTransform.position - transform.position;
        lookPos.y = lookRotY;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * Damp);
    }
}
