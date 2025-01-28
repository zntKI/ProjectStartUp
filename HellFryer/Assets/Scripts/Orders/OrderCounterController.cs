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
                if (container.placeIngedient(ingredient))
                {
                    return ingredient;
                }
            }
        }

        return null;
    }

    public override void CheckCooking(){}

    void removeIngredientsFromContainers()
    {
        foreach (IngredientContainer container in ingredientContainers)
        {
            container.removeIngredient();
        }
    }

    bool AreAllIngredientsPlaced()
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
}
