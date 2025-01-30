using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class CuttingBoardCookingBehaviour : CookingBehaviour
{
    public override void Cook(List<itemType> ingredients)
    {
        if (onCooked != null)
        {
            onCooked.Invoke(RecipeManager.instance.GetCookedFood(ingredients, itemType.Knife));
            

            StartCoroutine(CuttingSound());
        }
    }

    IEnumerator CuttingSound()
    {
        //SoundManager.instance.PlayKnifeOnCuttingBoard();
        yield return new WaitForSeconds(0.5f);

        //SoundManager.instance.StopKnifeOnCuttingBoard();
    }
}
