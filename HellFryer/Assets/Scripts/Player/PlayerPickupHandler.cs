using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickupHandler : MonoBehaviour
{
    PlayerController playerController;
    public Collider[] hitColliders;
    public List<GameObject> itemsInRange = new List<GameObject>();
    [SerializeField] float pickupRange = 1f;

    void UpdateItemsInRange()
    {
        itemsInRange.Clear();

        hitColliders = Physics.OverlapSphere(transform.position + transform.forward, pickupRange);
        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.tag == "Item")
            {
                itemsInRange.Add(hitCollider.gameObject);
            }
        }

        if (GetClosestItem() == null)
        {
            hitColliders = Physics.OverlapSphere(transform.position, pickupRange * 1.4f);
            foreach (Collider hitCollider in hitColliders)
            {
                if (hitCollider.gameObject.tag == "Item")
                {
                    itemsInRange.Add(hitCollider.gameObject);
                }
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
            ItemController item = itemObject.GetComponent<ItemController>();

            InventoryManager.instance.PickupItem(item, playerController.GetSelectedItemSlot());

            //ALEX
            //SoundManager.instance.PlayPickUpSound();
        }
    }

    public void PickupItem()
    {
        UpdateItemsInRange();
        PickupClosestItem();
    }

    public void SetPlayerController(PlayerController controller)
    {
        playerController = controller;
    }

    private void OnCollisionEnter(Collision collision)
    {
        ItemController item = collision.gameObject.GetComponent<ItemController>();

        if(item == null)
        {
            return;
        }

        InventoryManager.instance.PickupItem(item, playerController.GetSelectedItemSlot());

        SoundManager.instance.ItemPickup();
    }
}
