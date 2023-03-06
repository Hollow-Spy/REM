using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MapSelect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Image blockimg;
    [SerializeField] FontTyper LocationText;
    [SerializeField] string LocationName;
    public void OnPointerEnter(PointerEventData eventData)
    {
      
        blockimg.color = new Color(blockimg.color.r, blockimg.color.g, blockimg.color.b, 1);
        LocationText.Type(LocationName);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        blockimg.color = new Color(blockimg.color.r, blockimg.color.g, blockimg.color.b, 0);
        LocationText.ClearText();
    }


}
