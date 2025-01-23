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
        if (isEmpty())
        {
            ingredient = item;
            ingredient.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            ingredient.gameObject.transform.SetParent(gameObject.transform);
            ingredient.gameObject.transform.localScale = Vector3.one;
            ingredient.gameObject.tag = "Item";
            ingredient.gameObject.transform.position = gameObject.transform.position + itemHeight;

            return true;
        }
        else
        {
            return false;
        }
    }

    private void Update()
    {
        if(ingredient == null)
        {
            return;
        }

        if(ingredient.gameObject.transform.parent == null)
        {
            return;
        }

        if (ingredient.gameObject.transform.parent != gameObject.transform)
        {
            ingredient = null;
        }
    }

    //public void removeIngredient()
    //{
    //    if (ingredient != null) {
    //        ingredient.gameObject.GetComponent<Rigidbody>().isKinematic = false;
    //        Vector3 itemHeight = new Vector3(0, 1.2f, 0);
    //        ingredient.gameObject.transform.position = gameObject.transform.position + itemHeight;
    //        ingredient = null;
    //    }
    //}

    public void removeIngredient()
    {
        if (ingredient != null) {
            Destroy(ingredient.gameObject);
            ingredient = null;
        }
    }

    public bool isEmpty()
    {
        return ingredient == null;
    }
}
