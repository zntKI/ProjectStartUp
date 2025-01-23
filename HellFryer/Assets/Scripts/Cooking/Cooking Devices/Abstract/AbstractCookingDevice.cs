using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractCookingDevice : MonoBehaviour
{
    [SerializeField] protected List<IngredientContainer> ingredientContainers = new List<IngredientContainer>();

    public abstract bool placeIngredient(ItemController ingredient);
}
