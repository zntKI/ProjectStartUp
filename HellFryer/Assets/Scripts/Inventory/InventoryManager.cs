using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance { get; private set; }
    Dictionary<int, ItemController> items = new Dictionary<int, ItemController>(){
        { 0, null},
        { 1, null},
        { 2, null},
        { 3, null},
        { 4, null}
    };
    List<Transform> inventorySlots = new List<Transform>();

    public Transform ItemContent;
    public GameObject InventoryItem;

    public System.Action onPickup;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
            //DontDestroyOnLoad(this);

            foreach (Transform inventorySlot in ItemContent)
            {
                inventorySlots.Add(inventorySlot);
            }
        }
    }

    public void Add(ItemController item, int selectedSlot)
    {
        if (items[selectedSlot] == null)
        {
            //Add item to the first available inventory slot
            items[selectedSlot] = item;

            //Enable the inventory slot in the scene
            inventorySlots[selectedSlot].gameObject.SetActive(true);
        }
        else
        {
            //Add item to first empty slot
            foreach (int slot in items.Keys)
            {
                if (items[slot] == null)
                {
                    //Add item to the first available inventory slot
                    items[slot] = item;

                    //Enable the inventory slot in the scene
                    inventorySlots[slot].gameObject.SetActive(true);
                    break;
                }
            }
        }    

        UpdateItems();
    }

    public void PickupItem(ItemController item, int selectedSlot)
    {
        if (IsThereEmptySlot())
        {
            //Add item to inventory
            Add(item, selectedSlot);

            //Disable gameObject of the item in the scene
            item.gameObject.SetActive(false);
            item.gameObject.transform.SetParent(null);
        }
        //else
        //{
        //    //drop item on floor
        //    ItemController dropItem = items[selectedSlot];

        //    dropItem.gameObject.transform.position = item.transform.position;
        //    dropItem.gameObject.transform.SetParent(null);
        //    dropItem.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        //    dropItem.gameObject.GetComponent<Collider>().isTrigger = false;
        //    dropItem.gameObject.tag = "Item";
        //    dropItem.gameObject.SetActive(true);

        //    Remove(items[selectedSlot]);

        //    //Add item to inventory
        //    Add(item, selectedSlot);

        //    //Disable gameObject of the item in the scene
        //    item.gameObject.SetActive(false);
        //}
    }

    public void Remove(ItemController item)
    {
        foreach(int slot in items.Keys)
        {
            if(items[slot] == item)
            {
                items[slot] = null;
                inventorySlots[slot].gameObject.SetActive(false);
                break;
            }
        }

        UpdateItems();
    }

    public void LoseRandomItem()
    {
        System.Random r = new System.Random();
        foreach (int i in Enumerable.Range(0, 5).OrderBy(x => r.Next()))
        {
            if (items[i] == null)
            {
                continue;
            }

            EquipmentController equipmentController = items[i].gameObject.GetComponent<EquipmentController>();
            if (equipmentController == null)
            {
                Destroy(items[i].gameObject);
                items[i] = null;
                inventorySlots[i].gameObject.SetActive(false);
                break;
            }
        }

        UpdateItems();
    }

    public void UpdateItems()
    {
        foreach (int slot in items.Keys)
        {
            if (items[slot] != null)
            {
                GameObject obj = inventorySlots[slot].gameObject;
                TextMeshProUGUI itemName = obj.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
                UnityEngine.UI.Image itemIcon = obj.transform.Find("ItemIcon").GetComponent<UnityEngine.UI.Image>();

                itemName.text = items[slot].item.itemName;
                itemIcon.sprite = items[slot].item.icon;
            }
        }
    }

    public ItemController GetItem(int itemSlot)
    {
        return items[itemSlot];
    }

    bool IsThereEmptySlot()
    {
        foreach(ItemController item in items.Values)
        {
            if(item == null)
            {
                return true;
            }
        }

        return false;
    }
}
