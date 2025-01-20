using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class InventorySelector : MonoBehaviour
{
    public Transform SelectorContent;

    List<Transform> inventorySlots = new List<Transform>();
    int selectedSlot;

    void Start()
    {
        foreach (Transform inventorySlot in SelectorContent)
        {
            inventorySlots.Add(inventorySlot);
        }

        if(inventorySlots.Count > 0)
        {
            selectedSlot = 0;
            inventorySlots[selectedSlot].gameObject.SetActive(true);
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
            inventorySlots[selectedSlot].gameObject.SetActive(false);
            selectedSlot--;
            InventoryManager.instance.SetSelectedItemSlot(selectedSlot);
            inventorySlots[selectedSlot].gameObject.SetActive(true);
        }
    }

    void SelectRight()
    {
        if (selectedSlot < inventorySlots.Count - 1)
        {
            inventorySlots[selectedSlot].gameObject.SetActive(false);
            selectedSlot++;
            InventoryManager.instance.SetSelectedItemSlot(selectedSlot);
            inventorySlots[selectedSlot].gameObject.SetActive(true);
        }
    }
}
