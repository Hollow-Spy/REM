using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class OnHoverButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] GameObject OptionSelect, OptionDeSelect;
    [SerializeField] CursorSwitch cursor;
    public void OnPointerEnter(PointerEventData eventData)
    {
        OptionSelect.SetActive(true);
        OptionDeSelect.SetActive(false);
        cursor.SwitchSelect();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OptionSelect.SetActive(false);
        OptionDeSelect.SetActive(true);
        cursor.SwitchCursor();

    }
}
