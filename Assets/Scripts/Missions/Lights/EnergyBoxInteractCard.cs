using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBoxInteractCard : MonoBehaviour
{
    public bool CanActivate;
    [SerializeField] CanvasPopUpTrigger popcanvastrigger;
    [SerializeField] CanvasScreenSplit Splitter;
    [SerializeField] GameObject Typer1, Typer2,BulbIcon,LoginPrompt;
    public void Interaction()
    {
        if(CanActivate)
        {

            gameObject.layer = 0;
            Splitter.PopUpText("", popcanvastrigger.PopNum);

            StartCoroutine(Typing());
           
        }
        else
        {
            Splitter.PopUpText("Find a staff's ID card", popcanvastrigger.PopNum); 
        }
    }

    IEnumerator Typing()
    {
        Typer1.SetActive(true);
        yield return new WaitForSeconds(2);
        Typer2.SetActive(true);
        yield return new WaitForSeconds(2);
        BulbIcon.SetActive(true);
        LoginPrompt.SetActive(false);
    }
}
