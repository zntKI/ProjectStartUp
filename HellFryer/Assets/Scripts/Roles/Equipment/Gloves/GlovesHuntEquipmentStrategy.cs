using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlovesHuntEquipmentStrategy : EquipmentHuntStrategy
{
    [SerializeField]
    private float digDetectRange = 1f;

    public override void StartUp()
    {
        var hitColliders = Physics.OverlapSphere(transform.position, digDetectRange);
        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("ZombieArm"))
            {
                SoundManager.instance.PlayerHittingZombieWhileDigging();

                Destroy(hitCollider.transform.parent.gameObject); // Destroy zombie arm parent
                transform.parent.GetComponent<PlayerController>().EnableMovement(); // Assuming item is a child of the player

                SoundManager.instance.ZombieDeath();
            }
        }
    }

    public override void Perform()
    {
    }
}