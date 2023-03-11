using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SlotSelect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] int index = 0;
    [SerializeField] GameObject SlotSelector,SelectSound;
    [SerializeField] CursorSwitch cursor;
    public void OnPointerEnter(PointerEventData eventData)
    {
        cursor.SwitchSelect();
      
    }

    public void Click()
    {
       
            SlotSelector.transform.position = transform.position;
            SlotSelector.name = index.ToString();
            Instantiate(SelectSound, transform.position, Quaternion.identity);
        
    }


    public void OnPointerExit(PointerEventData eventData)
    {
        cursor.SwitchCursor();

    }
}
