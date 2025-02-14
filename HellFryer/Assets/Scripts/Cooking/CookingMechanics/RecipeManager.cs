using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class RecipeManager : MonoBehaviour
{
    public static RecipeManager instance { get; private set; }

    public List<IngredientRecipe> recipes = new List<IngredientRecipe>();

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
            recipes = GetComponents<IngredientRecipe>().ToList();
        }
    }

    public bool ContainsRecipe(List<itemType> recipe, itemType cookingEquipment)
    {
        foreach (IngredientRecipe curRecipe in recipes)
        {
            if (CompareRecipes(curRecipe.recipe, recipe) && curRecipe.cookingEquipment == cookingEquipment)
            {
                return true;
            }
        }

        return false;
    }

    public GameObject GetCookedFood(List<itemType> recipe, itemType cookingEquipment)
    {
        foreach (IngredientRecipe curRecipe in recipes)
        {
            if(CompareRecipes(curRecipe.recipe, recipe) && curRecipe.cookingEquipment == cookingEquipment)
            {
                return curRecipe.cookedFood;
            }
        }

        return null;
    }

    bool CompareRecipes(List<itemType> listA, List<itemType> listB)
    {
        listA = listA.OrderBy(x => x).ToList();

        return listA.SequenceEqual(listB.OrderBy(x => x).ToList());
    }
}
