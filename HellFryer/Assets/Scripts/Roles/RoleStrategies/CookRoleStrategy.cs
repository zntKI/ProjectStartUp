using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookRoleStrategy : RoleStrategy
{
    public override void OpenBook()
    {
        HandbookController.instance.ToggleMenu1Visibility();
    }

    public override void UpdateEquipmentType()
    {
        base.UpdateEquipmentType();
        if (equipmentController != null)
        {
            equipmentController.UpdateEquipmentType();
        }
    }
}