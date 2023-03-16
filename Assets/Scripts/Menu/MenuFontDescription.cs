using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuFontDescription : MonoBehaviour
{

    [SerializeField] FontTyper typer;
    [SerializeField] string Message;
    private void OnEnable()
    {
        typer.Type(Message);
    }

}
