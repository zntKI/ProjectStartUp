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

    public Transform ItemContent;
    public GameObject InventoryItem;

    public System.Action onPickup;

    Item selectedItem;

    //Ensure singleton
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
        selectedItem = item;
    }

    public void Remove(Item item)
    {
        if(selectedItem == item)
        {
            selectedItem = null;
        }
        items.Remove(item);
    }

    void Drop(Item item)
    {
        items[item].SetActive(true);
        Remove(item);
        UpdateItems();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (selectedItem != null)
            {
                Drop(selectedItem);
            }
        }
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
}
