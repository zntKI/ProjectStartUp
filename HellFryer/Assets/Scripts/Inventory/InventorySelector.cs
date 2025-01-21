using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class InventorySelector : MonoBehaviour
{
    public Transform SelectorContent;

    List<Transform> inventorySelectorSlots = new List<Transform>();
    int selectedSlot;

    void Start()
    {
        foreach (Transform inventorySlot in SelectorContent)
        {
            inventorySelectorSlots.Add(inventorySlot);
        }

        if(inventorySelectorSlots.Count > 0)
        {
            selectedSlot = 0;
            inventorySelectorSlots[selectedSlot].gameObject.SetActive(true);
        }
    }

    public void OnSelectLeft(CallbackContext context)
    {
        if (context.performed)
        {
            SelectLeft();
        }
    }

    public void OnSelectRight(CallbackContext context)
    {
        if (context.performed)
        {
            SelectRight();
        }
    }

    void SelectLeft()
    {
        if(selectedSlot > 0)
        {
            inventorySelectorSlots[selectedSlot].gameObject.SetActive(false);
            selectedSlot--;
            InventoryManager.instance.SetSelectedItemSlot(selectedSlot);
            inventorySelectorSlots[selectedSlot].gameObject.SetActive(true);
        }
    }

    void SelectRight()
    {
        if (selectedSlot < inventorySelectorSlots.Count - 1)
        {
            inventorySelectorSlots[selectedSlot].gameObject.SetActive(false);
            selectedSlot++;
            InventoryManager.instance.SetSelectedItemSlot(selectedSlot);
            inventorySelectorSlots[selectedSlot].gameObject.SetActive(true);
        }
    }
}
