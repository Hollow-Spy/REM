using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityManager : MonoBehaviour
{
    [SerializeField] GameObject Prompt,OkConfirmation ,LockdownText,UnlockedText,SearchTriggers;
    
    public void EnablePrompt()
    {
        Prompt.SetActive(true);
        SearchTriggers.SetActive(true);
        //enable triggers for search
    }
    public void EnableOkayButton()
    {
        OkConfirmation.SetActive(true);
    }

  
    public void OkayPressed()
    {
        UnlockedText.SetActive(true);
        LockdownText.SetActive(false);
        Prompt.SetActive(false);
    }

}
