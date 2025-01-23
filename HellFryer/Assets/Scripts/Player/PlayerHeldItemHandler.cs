using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.WSA;
using static UnityEditor.Progress;

public class PlayerHeldItemHandler : MonoBehaviour
{
    ItemController heldItem = null;

    [SerializeField]
    PlayerController playerController;

    [SerializeField] ItemController heldItem = null;
    float holdDistance = 0.8f;
    [SerializeField]
    float dropDistance = 1.2f;

    float placeIngredientRange = 0.5f;

    public void SetPlayerController(PlayerController controller)
    {
        playerController = controller;
    }

    void HoldItem(ItemController item)
    {
        if (heldItem == null)
        {
            GameObject curHeldItemObject = item.gameObject;
            Vector3 holdPosition = gameObject.transform.position + gameObject.transform.forward.normalized * holdDistance;
            curHeldItemObject.transform.position = holdPosition;
            curHeldItemObject.transform.SetParent(gameObject.transform);
            curHeldItemObject.GetComponent<Rigidbody>().isKinematic = true;
            curHeldItemObject.GetComponent<Collider>().isTrigger = true;
            curHeldItemObject.SetActive(true);
            curHeldItemObject.tag = "Untagged";

            heldItem = item;
            InventoryManager.instance.Remove(heldItem);
        }
        else
        {
            //Store previosly held item
            ItemController droppedItem = heldItem;

            //Hold the new item
            GameObject curHeldItemObject = item.gameObject;
            Vector3 holdPosition = gameObject.transform.position + gameObject.transform.forward.normalized * holdDistance;
            curHeldItemObject.transform.position = holdPosition;
            curHeldItemObject.transform.SetParent(gameObject.transform);
            curHeldItemObject.GetComponent<Rigidbody>().isKinematic = true;
            curHeldItemObject.GetComponent<Collider>().isTrigger = true;
            curHeldItemObject.SetActive(true);
            curHeldItemObject.tag = "Untagged";

            heldItem = item;
            InventoryManager.instance.Remove(heldItem);

            //Add the previous item to the inventory
            InventoryManager.instance.PickupItem(droppedItem, playerController.GetSelectedItemSlot());
        }
    }

    public void HoldSelectedItem()
    {
        ItemController selectedItem = InventoryManager.instance.GetItem(playerController.GetSelectedItemSlot());
        if (selectedItem != null)
        {
            HoldItem(selectedItem);
        }
    }

    public void DropHeldItem()
    {
        if (heldItem != null)
        {
            Vector3 dropPosition = gameObject.transform.position + gameObject.transform.forward.normalized * dropDistance;
            heldItem.gameObject.transform.position = dropPosition;
            heldItem.gameObject.transform.SetParent(null);
            heldItem.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            heldItem.gameObject.GetComponent<Collider>().isTrigger = false;
            heldItem.gameObject.tag = "Item";
            heldItem = null;
        }
    }

    public void PlaceIngredient()
    {
        Collider[] hitColliders = Physics.OverlapSphere(gameObject.transform.position + gameObject.transform.forward * placeIngredientRange * 2, placeIngredientRange);
        AbstractCookingDevice cookingDevice = GetClosestCookingDevice(hitColliders);

        if (heldItem != null)
        {
            //Check if heldItem is an ingredient
            if (cookingDevice != null && cookingDevice.placeIngredient(heldItem))
            {
                heldItem = null;
            }
        }
        else
        {
            if(cookingDevice != null)
            {
                cookingDevice.placeIngredient(null);
            }
        }
    }

    AbstractCookingDevice GetClosestCookingDevice(Collider[] collidersInRange)
    {
        float minDist = float.MaxValue;
        AbstractCookingDevice closestCookingDevice = null;
        foreach (Collider collider in collidersInRange)
        {
            GameObject curObject = collider.gameObject;
            if (curObject.tag != "CookingDevice") {
                continue;
            }

            AbstractCookingDevice cookingController = curObject.GetComponent<AbstractCookingDevice>();

            if (cookingController == null)
            {
                continue;   
            }

            float dist = Vector3.Distance(transform.position, curObject.transform.position);
            if (dist < minDist)
            {
                closestCookingDevice = cookingController;
            }
        }

        return closestCookingDevice;
    }

    public void RemoveParentFromItem(GameObject parent, ItemController heldItem)
    {
        if (heldItem != null)
        {
            Vector3 dropPosition = parent.transform.position + parent.transform.forward.normalized * dropDistance;
            heldItem.gameObject.transform.position = dropPosition;
            heldItem.gameObject.transform.SetParent(null);
            heldItem.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            heldItem.gameObject.GetComponent<Collider>().isTrigger = false;
            heldItem.gameObject.tag = "Item";
            heldItem = null;
        }
    }

    public void SetParentToItem(GameObject parent, ItemController item)
    {
        GameObject itemObject = item.gameObject;
        Vector3 holdPosition = parent.transform.position + parent.transform.forward.normalized * holdDistance;
        itemObject.transform.position = holdPosition;
        itemObject.transform.SetParent(parent.transform);
        itemObject.GetComponent<Rigidbody>().isKinematic = true;
        itemObject.GetComponent<Collider>().isTrigger = true;
        itemObject.SetActive(true);
        itemObject.tag = "Untagged";
    }
}
