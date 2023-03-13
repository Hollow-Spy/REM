using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class FontTyper : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textComp;
    string message;
    IEnumerator Typer;
    bool isTyping;

    public bool IsTpying()
    {
        return isTyping;
    }

    public void ClearText()
    {
        if (isTyping)
        {
            StopCoroutine(Typer);
            isTyping = false;
        }
        textComp.text = "";

    }

    public void Type(string msg)
    {
        ClearText();
        message = msg;

        Typer = TypeNumerator();
        StartCoroutine(Typer);
    }

    IEnumerator TypeNumerator()
    {
        isTyping = true;
        int increment = 0;
        while(textComp.text.Length < message.Length )
        {
            increment++;
            yield return new WaitForSeconds(.025f);
            textComp.text = message.Substring(0, increment);

          
            if(textComp.text[increment-1] == ',')
            {
                yield return new WaitForSeconds(.5f);
            }
            if (textComp.text[increment-1] == '.')
            {
                yield return new WaitForSeconds(1f);
            }
            if (textComp.text[increment - 1] == '?')
            {
                yield return new WaitForSeconds(1.5f);
            }
        }
        textComp.text = message;
        isTyping = false;

    }

}
