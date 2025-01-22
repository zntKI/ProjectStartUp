using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CookingBehaviour : MonoBehaviour
{
    public abstract void Cook(List<ItemController> ingredients);
}
