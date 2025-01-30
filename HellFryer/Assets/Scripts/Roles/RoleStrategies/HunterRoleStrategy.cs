using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterRoleStrategy : RoleStrategy
{
    public override void OpenBook(GameObject book)
    {
        //HandbookController.instance.ToggleMenu2Visibility();
    }

    public override void UpdateEquipmentType()
    {
        base.UpdateEquipmentType();
        if (equipmentController != null)
        {
            equipmentController.SwitchEquipmentType();
        }
    }
}