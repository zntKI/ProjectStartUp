using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class StoveController : AbstractCookingDevice
{
    StoveCookingBehaviour cookingBehaviour;
    [SerializeField] IngredientContainer panContainer;

    private void Start()
    {
        cookingBehaviour = GetComponent<StoveCookingBehaviour>();
        cookingBehaviour.onCooked += MakeCookedFood;
    }

    private void StartCooking()
    {
        List<itemType> ingredientList = GetIngredients();

        if (RecipeManager.instance.ContainsRecipe(ingredientList, itemType.Pan))
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

        if (ingredient.item.itemType == itemType.Pan)
        {
            if (panContainer.placeIngredient(ingredient))
            {
                return ingredient;
            }
        }

        if (ingredient.GetComponent<EquipmentController>() != null)
        {
            return null;
        }

        if (!AreAllIngredientsPlaced() && ingredient.item.itemType != itemType.Pan)
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
        if (AreAllIngredientsPlaced() && IsPanPlaced())
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

    bool IsPanPlaced()
    {
        if (!panContainer.isEmpty())
        {
            return true;
        }

        return false;
    }

    void MakeCookedFood(GameObject _cookedFood)
    {
        if(_cookedFood == null)
        {
            return;
        }

        Vector3 spawnPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 1.5f, gameObject.transform.position.z);
        cookedFood = Instantiate(_cookedFood, spawnPos, Quaternion.identity);

        //if(cookedFood.GetComponent<ItemController>() != null)
        //{
        //    ingredientContainers[1].placeIngredient(cookedFood.GetComponent<ItemController>());
        //}
        cookedFood.SetActive(true);
        cookedFood = null;
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
