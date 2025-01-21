using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookRoleStrategy : RoleStrategy
{
    public override void OpenBook()
    {
        Debug.Log("Opening book does not do anything yet!!!");
    }

    public override void PerformTask()
    {
        throw new System.NotImplementedException();
    }
}