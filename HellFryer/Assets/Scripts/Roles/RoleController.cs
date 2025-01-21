using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

/// <summary>
/// Does the heavy-lifting of role switching:<br></br>
/// Communicates with the strategy controllers to retrieve current strategy<br></br>
/// </summary>
[RequireComponent(typeof(RoleStrategyController))]
public class RoleController : MonoBehaviour
{
    private RoleStrategyController roleStrategyController;

    private RoleStrategy currentRoleStrategy;

    void Start()
    {
        roleStrategyController = GetComponent<RoleStrategyController>();

        roleStrategyController.OnStrategyEnabled += CheckChangingRoleStrategy;

        // Retrieves it from the Start so that when SetupSpawning is called it does not do an early return
        currentRoleStrategy = roleStrategyController.CurrentRoleStrategy;
    }

    public void PerformTask()
    {
        currentRoleStrategy.PerformTask();
    }

    public void OpenBook()
    {
        currentRoleStrategy.OpenBook();
    }

    /// <summary>
    /// Enables switching between role strategies
    /// </summary>
    void CheckChangingRoleStrategy()
    {
        currentRoleStrategy = roleStrategyController.CurrentRoleStrategy;
    }

    void OnDestroy()
    {
        roleStrategyController.OnStrategyEnabled -= CheckChangingRoleStrategy;
    }
}