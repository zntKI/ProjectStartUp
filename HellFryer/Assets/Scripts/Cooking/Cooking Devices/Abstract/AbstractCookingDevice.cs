using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractCookingDevice : MonoBehaviour
{
    [SerializeField] protected List<IngredientContainer> ingredientContainers = new List<IngredientContainer>();
    protected GameObject cookedFood;
    public abstract bool placeIngredient(ItemController ingredient);
}
