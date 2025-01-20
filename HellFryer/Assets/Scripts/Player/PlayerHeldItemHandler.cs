using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Progress;

public class PlayerHeldItemHandler : MonoBehaviour
{
    Item heldItem = null;
    [SerializeField] GameObject heldItemObject = null;
    float holdDistance = 0.8f;
    float dropDistance = 1.2f;

    void HoldItem(Item item, GameObject holder)
    {
        heldItem = item;
        heldItemObject = InventoryManager.instance.items[item];
        Vector3 holdPosition = holder.transform.position + holder.transform.forward.normalized * holdDistance;
        heldItemObject.transform.position = holdPosition;
        heldItemObject.transform.SetParent(holder.transform);
        heldItemObject.GetComponent<Rigidbody>().isKinematic = true;
        heldItemObject.GetComponent<SphereCollider>().isTrigger = true;
        heldItemObject.SetActive(true);

        InventoryManager.instance.Remove(heldItem);
    }

    public void HoldSelectedItem(GameObject holder)
    {
        Item selectedItem = InventoryManager.instance.GetSelectedItem();
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
            heldItemObject.transform.position = dropPosition;
            heldItemObject.transform.SetParent(null);
            heldItemObject.GetComponent<Rigidbody>().isKinematic = false;
            heldItemObject.GetComponent<SphereCollider>().isTrigger = false;
            heldItem = null;
            heldItemObject = null;
        }
    }
}
