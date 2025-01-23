using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;

public class OvenController : AbstractCookingDevice
{
    OvenCookingBehaviour cookingBehaviour;

    private void Start()
    {
        cookingBehaviour = GetComponent<OvenCookingBehaviour>();
        cookingBehaviour.onCooked += SpawnCookedFood;
    }

    private void StartCooking()
    {
        List<itemType> ingredientList = GetIngredients();

        if (RecipeManager.instance.ContainsRecipe(ingredientList))
        {
            cookingBehaviour.Cook(ingredientList);
            removeIngredientsFromContainers();
        }
        else
        {
            Debug.Log("Incorrect recipe");
        }
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

    void SpawnCookedFood(GameObject cookedFood)
    {
        cookedFood = Instantiate(cookedFood, gameObject.transform);
        ingredientContainers[0].placeIngedient(cookedFood.GetComponent<ItemController>());
        cookingBehaviour.onCooked -= SpawnCookedFood;
    }

    List<itemType> GetIngredients()
    {
        List<itemType> ingredients = new List<itemType>();

        foreach (IngredientContainer container in ingredientContainers)
        {
            if (!container.isEmpty())
            {
                ingredients.Add(container.ingredient.item.itemType);
            }
        }

        return ingredients;
    }
}
