using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;

public class CuttingBoardController : AbstractCookingDevice
{
    CuttingBoardCookingBehaviour cookingBehaviour;

    private void Start()
    {
        cookingBehaviour = GetComponent<CuttingBoardCookingBehaviour>();
        cookingBehaviour.onCooked += MakeCookedFood;
    }

    private void StartCooking()
    {
        List<itemType> ingredientList = GetIngredients();

        if (RecipeManager.instance.ContainsRecipe(ingredientList, itemType.Knife))
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
        if (ingredient == null)
        {
            return null;
        }

        if (ingredient.GetComponent<EquipmentController>() != null)
        {
            return null;
        }

        if (!AreAllIngredientsPlaced() && ingredient)
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

    public override void CheckCooking()
    {
        //if (AreAllIngredientsPlaced())
        //{
        //    StartCooking();
        //}
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
        if(_cookedFood == null)
        {
            return;
        }

        cookedFood = Instantiate(_cookedFood, gameObject.transform);
        cookedFood.SetActive(false);

        if(cookedFood.GetComponent<ItemController>() != null)
        {
            ingredientContainers[1].placeIngredient(cookedFood.GetComponent<ItemController>());
        }
    }

    public void CookFood()
    {
        if (AreAllIngredientsPlaced())
        {
            StartCooking();
        }

        if (cookedFood != null)
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
