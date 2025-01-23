using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlovesCookEquipmentStrategy : EquipmentStrategy
{
    float pickupRange = 1;
    OvenController ovenController;

    public override void StartUp()
    {
        Debug.Log("sdsadsad");
        ovenController = GetOven();
        if(ovenController == null)
        {
            return;
        }

        ovenController.TakeOutCookedFood();

    }

    OvenController GetOven()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position + transform.forward, pickupRange);
        foreach (Collider hitCollider in hitColliders)
        {
            if(hitCollider.gameObject.tag != "CookingDevice")
            {
                continue;
            }

            if (hitCollider.gameObject.GetComponent<OvenController>() == null)
            {
                continue;
            }

            return hitCollider.gameObject.GetComponent<OvenController>();
        }

        return null;
    }

    public override void Perform()
    {
        throw new System.NotImplementedException();
    }
}
