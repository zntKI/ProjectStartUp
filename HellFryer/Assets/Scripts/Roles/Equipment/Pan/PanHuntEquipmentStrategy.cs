using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanHuntEquipmentStrategy : EquipmentHuntStrategy
{
    [SerializeField]
    private float hitDetectRange = 3f;

    public override void StartUp()
    {
        var hitColliders = Physics.OverlapSphere(transform.position, hitDetectRange);
        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Projectile")) // If "hit" projectile with pan:
            {
                ProjectileController projectile = hitCollider.GetComponent<ProjectileController>();
                projectile.SwitchMoveDir(transform.parent.forward); // Send projectile outwards from the player in his current forward dir
            }
        }
    }

    public override void Perform()
    {
    }
}