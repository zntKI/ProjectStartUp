using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.WSA;
using static UnityEditor.Progress;

public class PlayerHeldItemHandler : MonoBehaviour
{
    PlayerController playerController;

    ItemController heldItem = null;
    float holdDistance = 0.8f;
    float dropDistance = 1.2f;

    void HoldItem(ItemController item, GameObject holder)
    {
        if (heldItem == null)
        {
            GameObject curHeldItemObject = item.gameObject;
            Vector3 holdPosition = holder.transform.position + holder.transform.forward.normalized * holdDistance;
            curHeldItemObject.transform.position = holdPosition;
            curHeldItemObject.transform.SetParent(holder.transform);
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
            Vector3 holdPosition = holder.transform.position + holder.transform.forward.normalized * holdDistance;
            curHeldItemObject.transform.position = holdPosition;
            curHeldItemObject.transform.SetParent(holder.transform);
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

    public void HoldSelectedItem(GameObject holder)
    {
        ItemController selectedItem = InventoryManager.instance.GetItem(playerController.GetSelectedItemSlot());
        if (selectedItem != null)
        {
            HoldItem(selectedItem, holder);
        }
    }

    public void DropHeldItem(GameObject holder)
    {
        if (heldItem != null)
        {
            Vector3 dropPosition = holder.transform.position + holder.transform.forward.normalized * dropDistance;
            heldItem.gameObject.transform.position = dropPosition;
            heldItem.gameObject.transform.SetParent(null);
            heldItem.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            heldItem.gameObject.GetComponent<Collider>().isTrigger = false;
            heldItem.gameObject.tag = "Item";
            heldItem = null;
        }
    }

    public void SetPlayerController(PlayerController controller)
    {
        playerController = controller;
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
