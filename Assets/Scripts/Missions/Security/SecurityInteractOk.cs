using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityInteractOk : MonoBehaviour
{
    [SerializeField] SecurityManager manager;
    public void Interaction()
    {
        gameObject.tag = "Untagged";
        manager.OkayPressed();

    }
}
