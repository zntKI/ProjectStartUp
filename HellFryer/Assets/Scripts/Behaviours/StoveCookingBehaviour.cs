using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class StoveCookingBehaviour : CookingBehaviour
{
    float cookingTime = 1f;

    public override void Cook(List<itemType> ingredients)
    {
        StartCoroutine(CookingCoroutine(ingredients, itemType.Pan));
    }

    IEnumerator CookingCoroutine(List<itemType> ingredients, itemType cookingEquipment)
    {
        yield return new WaitForSeconds(cookingTime);

        if(onCooked != null)
        {
            onCooked.Invoke(RecipeManager.instance.GetCookedFood(ingredients, cookingEquipment));
        }
    }
}
