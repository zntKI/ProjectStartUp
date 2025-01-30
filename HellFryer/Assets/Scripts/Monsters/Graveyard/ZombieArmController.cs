using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieArmController : MonoBehaviour
{
    [SerializeField]
    private float timeBetweenItemDestroy;
    private float timeCounter = 0f;

    void Update()
    {
        timeCounter += Time.deltaTime;
        if (timeCounter > timeBetweenItemDestroy)
        {
            InventoryManager.instance.LoseRandomItem();
            timeCounter = 0f;
        }
    }
}
