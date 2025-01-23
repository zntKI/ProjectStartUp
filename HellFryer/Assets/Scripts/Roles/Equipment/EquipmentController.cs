using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Does the heavy-lifting of equipment performing:<br></br>
/// Communicates with the strategy controllers to retrieve current strategy<br></br>
/// </summary>
[RequireComponent(typeof(EquipmentStrategyController))]
public class EquipmentController : MonoBehaviour
{
    private EquipmentStrategyController equipmentStrategyController;

    private EquipmentStrategy currentEquipmentStrategy;

    void Start()
    {
        equipmentStrategyController = GetComponent<EquipmentStrategyController>();

        equipmentStrategyController.OnStrategyEnabled += CheckChangingRoleStrategy;

        // Retrieves it from the Start so that when SetupSpawning is called it does not do an early return
        currentEquipmentStrategy = equipmentStrategyController.CurrentEquipmentStrategy;
    }

    public void Perform()
    {
        currentEquipmentStrategy.StartUp();
    }

    public void UpdateEquipmentType()
    {
        equipmentStrategyController.SwitchEquipmentType();
    }

    /// <summary>
    /// Enables switching between role strategies
    /// </summary>
    void CheckChangingRoleStrategy()
    {
        currentEquipmentStrategy = equipmentStrategyController.CurrentEquipmentStrategy;
    }

    void OnDestroy()
    {
        equipmentStrategyController.OnStrategyEnabled -= CheckChangingRoleStrategy;
    }
}
