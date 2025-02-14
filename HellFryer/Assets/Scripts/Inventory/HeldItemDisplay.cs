using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HeldItemDisplay : MonoBehaviour
{
    public GameObject heldItemSlot;
    
    public void SetItem(ItemController itemController)
    {
        if (heldItemSlot == null)
            return;

        TextMeshProUGUI itemName = heldItemSlot.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
        UnityEngine.UI.Image itemIcon = heldItemSlot.transform.Find("ItemIcon").GetComponent<UnityEngine.UI.Image>();

        itemName.text = itemController.item.itemName;
        itemIcon.sprite = itemController.item.icon;

        heldItemSlot.SetActive(true);
    }

    public void RemoveItem()
    {
        if (heldItemSlot == null)
            return;

        heldItemSlot.SetActive(false);
    }
}
