using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterRoleStrategy : RoleStrategy
{
    public override void OpenBook()
    {
        Debug.Log("Opening book does not do anything yet!!!");
    }
    
    // For now only throws a knife
    // TODO: Abstract all types of equipment into different classes with base abstract class Equipment
    // and call their methods (Perfrom f.ex.) according to the specific type of equipment it is.
    public override void PerformTask()
    {
        Debug.Log("Throw knife");
    }
}