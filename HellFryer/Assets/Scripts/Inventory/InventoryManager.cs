using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.WSA;
using static UnityEditor.Progress;

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

    public int selectedSlot = 0;
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

    public void Add(ItemController item)
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

    public void PickupItem(ItemController item)
    {
        if (isThereEmptySlot())
        {
            //Add item to inventory
            Add(item);

            //Disable gameObject of the item in the scene
            item.gameObject.SetActive(false);
        }
        else
        {
            //drop item on floor
            ItemController dropItem = items[selectedSlot];

            dropItem.gameObject.transform.position = item.transform.position;
            dropItem.gameObject.transform.SetParent(null);
            dropItem.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            dropItem.gameObject.GetComponent<Collider>().isTrigger = false;
            dropItem.gameObject.tag = "Item";
            dropItem.gameObject.SetActive(true);

            Remove(items[selectedSlot]);

            //Add item to inventory
            Add(item);

            //Disable gameObject of the item in the scene
            item.gameObject.SetActive(false);
        }
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

    public void SetSelectedItemSlot(int slot)
    {
        selectedSlot = slot;
    }

    public ItemController GetSelectedItem()
    {
        return items[selectedSlot];
    }

    bool isThereEmptySlot()
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
