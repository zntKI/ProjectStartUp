using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;

public class OrderCounterController : AbstractCookingDevice
{
    public override ItemController placeIngredient(ItemController ingredient)
    {
        if(ingredient.GetComponent<EquipmentController>() != null)
        {
            return null;
        }

        if (!AreAllIngredientsPlaced() && ingredient != null)
        {
            foreach (IngredientContainer container in ingredientContainers)
            {
                if (container.placeIngredient(ingredient))
                {
                    return ingredient;
                }
            }
        }

        return null;
    }

    public override void CheckCooking(){}

    public void removeIngredientsFromContainers()
    {
        foreach (IngredientContainer container in ingredientContainers)
        {
            container.removeIngredient();
        }
    }

    public bool AreAllIngredientsPlaced()
    {
        foreach (IngredientContainer container in ingredientContainers)
        {
            if (container.isEmpty())
            {
                return false;
            }
        }

        return true;
    }

    public bool CompareFood(itemType food)
    {
        foreach (IngredientContainer container in ingredientContainers)
        {
            if (container.isEmpty())
            {
                return false;
            }
            else
            {
                return container.ingredient.item.itemType == food;
            }
        }

        return false;
    }
}
