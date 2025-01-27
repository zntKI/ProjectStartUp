using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.WSA;
using static UnityEditor.Progress;

public class PlayerHeldItemHandler : MonoBehaviour
{
    [SerializeField]
    PlayerController playerController;

    public ItemController heldItem = null;
    [SerializeField] float holdDistance = 0.8f;
    [SerializeField] float dropDistance = 1.2f;

    float placeIngredientRange = 1.1f;

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

    public ItemController PlaceIngredient()
    {
        ItemController placedIngredient = null;

        Collider[] hitColliders = Physics.OverlapSphere(gameObject.transform.position + gameObject.transform.forward, placeIngredientRange);
        AbstractCookingDevice cookingDevice = GetClosestCookingDevice(transform, hitColliders);

        if (IsHoldingIngredient())
        {
            if (cookingDevice != null && cookingDevice.placeIngredient(heldItem) != null)
            {
                placedIngredient = heldItem;
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

        return placedIngredient;
    }

    bool IsHoldingIngredient()
    {
        return heldItem != null && heldItem.GetComponent<EquipmentController>() == null;
    }

    public AbstractCookingDevice GetClosestCookingDevice(Transform player, Collider[] collidersInRange)
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

            float dist = Vector3.Distance(player.position, curObject.transform.position);
            if (dist < minDist)
            {
                closestCookingDevice = cookingController;
            }
        }

        return closestCookingDevice;
    }
}
