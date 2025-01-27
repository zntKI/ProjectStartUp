using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class OvenCookingBehaviour : CookingBehaviour
{
    float cookingTime = 1f;

    public override void Cook(List<itemType> ingredients)
    {
        StartCoroutine(CookingCoroutine(ingredients));
    }

    IEnumerator CookingCoroutine(List<itemType> ingredients)
    {
        yield return new WaitForSeconds(cookingTime);

        if(onCooked != null)
        {
            onCooked.Invoke(RecipeManager.instance.GetCookedFood(ingredients));
        }
    }
}
