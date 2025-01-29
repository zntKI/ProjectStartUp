using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class IngredientContainer : MonoBehaviour
{
    public ItemController ingredient;

    Vector3 itemHeight = new Vector3(0, 0.5f, 0);
    public bool placeIngedient(ItemController item)
    {
        if (isEmpty() && item != null)
        {
            ingredient = item;
            ingredient.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            ingredient.gameObject.transform.SetParent(gameObject.transform);
            ingredient.gameObject.transform.localScale = Vector3.one;
            ingredient.gameObject.tag = "Item";
            ingredient.gameObject.transform.position = gameObject.transform.position + itemHeight;

            ActivateHostileIngredient();

            return true;
        }
        else
        {
            return false;
        }
    }

    private void Update()
    {
        if (ingredient == null)
        {
            return;
        }

        if (ingredient.gameObject.transform.parent == null)
        {
            DeactivateHostileIngredient();
            ingredient = null;
            return;
        }

        if (ingredient.gameObject.transform.parent != gameObject.transform)
        {
            DeactivateHostileIngredient();
            ingredient = null;
        }
    }

    public void removeIngredient()
    {
        if (ingredient != null) {
            DeactivateHostileIngredient();
            Destroy(ingredient.gameObject);
            ingredient = null;
        }
    }

    public bool isEmpty()
    {
        return ingredient == null;
    }

    void ActivateHostileIngredient()
    {
        HostileItemController hostileItemController = ingredient.gameObject.GetComponent<HostileItemController>();
        HostileItemController hostileItemControllerInChildren = ingredient.gameObject.GetComponentInChildren<HostileItemController>();

        if (hostileItemController != null)
        {
            hostileItemController.Activate();
        }else if(hostileItemControllerInChildren != null)
        {
            hostileItemControllerInChildren.Activate();
        }
    }

    void DeactivateHostileIngredient()
    {
        HostileItemController hostileItemController = ingredient.gameObject.GetComponent<HostileItemController>();
        HostileItemController hostileItemControllerInChildren = ingredient.gameObject.GetComponentInChildren<HostileItemController>();

        if (hostileItemController != null)
        {
            hostileItemController.Deactivate();
        }
        else if (hostileItemControllerInChildren != null)
        {
            hostileItemControllerInChildren.Deactivate();
        }
    }
}
