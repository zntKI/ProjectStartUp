using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OvenController : AbstractCookingDevice
{
    OvenCookingBehaviour cookingBehaviour;

    private void Start()
    {
        cookingBehaviour = GetComponent<OvenCookingBehaviour>();
    }
    private void StartCooking()
    {
        cookingBehaviour.Cook(GetIngredients());
        removeIngredientsFromContainers();
    }

    public override bool placeIngredient(ItemController ingredient)
    {
        if (AreAllIngredientsPlaced())
        {
            StartCooking();

            return false;
        }
        else if(ingredient != null)
        {
            foreach (IngredientContainer container in ingredientContainers)
            {
                if (container.placeIngedient(ingredient))
                {
                    return true;
                }
            }
        }

        return false;
    }

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

    List<ItemController> GetIngredients()
    {
        List<ItemController> ingredients = new List<ItemController>();

        foreach (IngredientContainer container in ingredientContainers)
        {
            if (!container.isEmpty())
            {
                ingredients.Add(container.ingredient);
            }
        }

        return ingredients;
    }
}
