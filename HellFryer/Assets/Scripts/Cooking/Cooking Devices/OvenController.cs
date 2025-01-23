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
        cookingBehaviour.onCooked += MakeCookedFood;
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

    public override ItemController placeIngredient(ItemController ingredient)
    {
        if(!AreAllIngredientsPlaced() && ingredient != null)
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

    public override void CheckCooking()
    {
        if (AreAllIngredientsPlaced())
        {
            StartCooking();
        }
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

    void MakeCookedFood(GameObject _cookedFood)
    {
        cookedFood = Instantiate(_cookedFood, gameObject.transform);
        cookedFood.SetActive(false);
        ingredientContainers[1].placeIngedient(cookedFood.GetComponent<ItemController>());
    }

    public void TakeOutCookedFood()
    {
        if(cookedFood != null)
        {
            cookedFood.SetActive(true);
            cookedFood = null;
        }
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

    private void OnDestroy()
    {
        cookingBehaviour.onCooked -= MakeCookedFood;
    }
}
