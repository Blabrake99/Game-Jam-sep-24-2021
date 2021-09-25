using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class InventorySlots : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string ItemDescription;

    public Text DescriptionBox;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(ItemDescription != "")
            DescriptionBox.text = ItemDescription;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (ItemDescription != "")
            DescriptionBox.text = "";
    }
}
