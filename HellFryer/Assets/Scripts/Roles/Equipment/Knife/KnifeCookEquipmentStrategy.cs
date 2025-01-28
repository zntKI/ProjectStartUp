using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeCookEquipmentStrategy : EquipmentStrategy
{
    float pickupRange = 1;
    CuttingBoardController cuttingBoardController;

    public override void StartUp()
    {
        cuttingBoardController = GetCuttingBoard();

        if (cuttingBoardController == null)
        {
            return;
        }

        cuttingBoardController.CookFood();
    }

    CuttingBoardController GetCuttingBoard()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position + transform.forward, pickupRange);
        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.tag != "CookingDevice")
            {
                continue;
            }

            if (hitCollider.gameObject.GetComponent<CuttingBoardController>() == null)
            {
                continue;
            }

            return hitCollider.gameObject.GetComponent<CuttingBoardController>();
        }

        return null;
    }

    public override void Perform()
    {
        throw new System.NotImplementedException();
    }
}
