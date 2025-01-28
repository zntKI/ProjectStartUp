using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Responsible for the change in role strategies
/// </summary>
[RequireComponent(typeof(RoleStrategy))]
public class RoleStrategyController : StrategyController
{
    public RoleStrategy CurrentRoleStrategy
        => currentRoleStrategy;

    [SerializeField]
    private RoleStrategy currentRoleStrategy;

    private RoleStrategy previousRoleStrategy; // used to check against disabling all strategies at once

    void Start()
    {
        if (currentRoleStrategy == null) // If not specified in the inspector
        {
            currentRoleStrategy = GetComponents<RoleStrategy>().FirstOrDefault(w => w.enabled);
        }
    }

    public void SwitchRoles()
    {
        RoleStrategy otherRoleStrategy = GetComponents<RoleStrategy>().FirstOrDefault(r => !r.enabled);
        if (otherRoleStrategy == null)
        {
            Debug.LogError("No other disabled role strategy");
            return;
        }
        else if (otherRoleStrategy == currentRoleStrategy)
        {
            Debug.LogError("Other role strategy the same with the current one");
            return;
        }

        currentRoleStrategy.enabled = false;
        currentRoleStrategy = otherRoleStrategy;
        currentRoleStrategy.enabled = true;
    }

    /// <summary>
    /// Checks new strategy when enabled
    /// </summary>
    public override void EnableStrategy(Strategy newStrategy)
    {
        if (newStrategy is not RoleStrategy) // Due to the event being static
            return;

        var newRoleStrategy = (RoleStrategy)newStrategy;

        if (newRoleStrategy == currentRoleStrategy) // Avoid Unity start-up calls
            return;

        previousRoleStrategy = currentRoleStrategy;
        currentRoleStrategy = newRoleStrategy;

        // Check if change of strategies happened while spawning takes place
        StrategyEnabled();

        // Only for the inspector
        if (previousRoleStrategy != null)
            previousRoleStrategy.enabled = false;
    }

    public override void DisableStrategy(Strategy strategy)
    {
        if (strategy is not RoleStrategy)
            return;

        // Prohibit it - only switching strategies should be allowed because the player should have a role at all times
        if (strategy != previousRoleStrategy)
        {
            //Debug.LogWarning("Should not disable all strategies at once - try switching between them instead!");
        }
    }
}