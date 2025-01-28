using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class IngredientRecipe : MonoBehaviour
{
    public List<itemType> recipe = new List<itemType>();

    public itemType cookingEquipment;

    public GameObject cookedFood;
}
