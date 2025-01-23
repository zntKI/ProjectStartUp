using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EquipmentStrategy : Strategy
{
    public abstract void StartUp();
    public abstract void Perform();
}