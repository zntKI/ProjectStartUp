using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class PlayerPickupHandler : MonoBehaviour
{
    Collider[] hitColliders;
    public List<GameObject> itemsInRange = new List<GameObject>();
    const int pickupRange = 2;

    void UpdateItemsInRange()
    {
        itemsInRange.Clear();

        hitColliders = Physics.OverlapSphere(transform.position, pickupRange);
        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.tag == "Item")
            {
                itemsInRange.Add(hitCollider.gameObject);
            }
        }
    }

    GameObject GetClosestItem()
    {
        float minDist = float.MaxValue;
        GameObject closestItem = null;
        foreach (GameObject item in itemsInRange)
        {
            float dist = Vector3.Distance(transform.position, item.transform.position);
            if (dist < minDist)
            {
                closestItem = item;
            }
        }

        return closestItem;
    }

    void PickupClosestItem()
    {
        GameObject itemObject = GetClosestItem();
        if (itemObject != null)
        {
            Item item = itemObject.GetComponent<ItemController>().item;

            InventoryManager.instance.PickupItem(item, itemObject);
        }
    }

    public void PickupItem()
    {
        UpdateItemsInRange();
        PickupClosestItem();
    }
}
