using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class OnHoverText : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] string Text;
    [SerializeField] FontTyper prompt;
    [SerializeField] CursorSwitch cursor;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        prompt.Type(Text);
        cursor.SwitchSelect();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        prompt.ClearText();
        cursor.SwitchCursor();

    }
}
