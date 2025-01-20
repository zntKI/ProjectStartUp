using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static UnityEditor.Progress;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance { get; private set; }
    public Dictionary<Item, GameObject> items = new Dictionary<Item, GameObject>();
    Dictionary<int, Item> itemSlots = new Dictionary<int, Item>(){
        { 0, null},
        { 1, null},
        { 2, null},
        { 3, null},
        { 4, null}
    };

    public Transform ItemContent;
    public GameObject InventoryItem;

    public System.Action onPickup;

    public int selectedItem = 0;
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
        }
    }

    public void Add(Item item, GameObject itemObejct)
    {
        items.Add(item, itemObejct);
        AddItemToItemSlot(item);
        UpdateItems();
    }

    public void PickupItem(Item item, GameObject itemObejct)
    {
        Add(item, itemObejct);
        UpdateItems();

        itemObejct.SetActive(false);
    }

    public void Remove(Item item)
    {
        if (itemSlots[selectedItem] == item)
        {
            itemSlots[selectedItem] = null;
        }
        items.Remove(item);
        UpdateItems();
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Q))
        //{
        //    if (selectedItem != null)
        //    {
        //        Drop(selectedItem);
        //    }
        //}
    }

    public void UpdateItems()
    {
        EmptyInventorySlots();

        foreach (Item item in items.Keys)
        {
            GameObject obj = Instantiate(InventoryItem, ItemContent);
            TextMeshProUGUI itemName = obj.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
            UnityEngine.UI.Image itemIcon = obj.transform.Find("ItemIcon").GetComponent<UnityEngine.UI.Image>();

            itemName.text = item.itemName;
            itemIcon.sprite = item.icon;
        }
    }

    void EmptyInventorySlots()
    {
        foreach (Transform item in ItemContent)
        {
            Destroy(item.gameObject);
        }
    }

    void AddItemToItemSlot(Item item)
    {
        foreach(int slot in itemSlots.Keys)
        {
            if(itemSlots[slot] == null)
            {
                itemSlots[slot] = item;
                break;
            }
        }
    }

    public void SetSelectedItemSlot(int slot)
    {
        selectedItem = slot;
    }

    public Item GetSelectedItem()
    {
        return itemSlots[selectedItem];
    }
}
