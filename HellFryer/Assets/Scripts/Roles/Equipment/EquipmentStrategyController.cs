using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Responsible for the change in equipment strategies
/// </summary>
[RequireComponent(typeof(EquipmentStrategy))]
public class EquipmentStrategyController : StrategyController
{
    public EquipmentStrategy CurrentEquipmentStrategy
        => currentEquipmentStrategy;

    [SerializeField]
    private EquipmentStrategy currentEquipmentStrategy;

    private EquipmentStrategy previousEquipmentStrategy; // used to check against disabling all strategies at once

    void Start()
    {
        if (currentEquipmentStrategy == null) // If not specified in the inspector
        {
            currentEquipmentStrategy = GetComponents<EquipmentStrategy>().FirstOrDefault(w => w.enabled);
        }
    }

    public void SwitchEquipmentType()
    {
        EquipmentStrategy otherEquipmentStrategy = GetComponents<EquipmentStrategy>().FirstOrDefault(e => !e.enabled);
        if (otherEquipmentStrategy == null)
        {
            Debug.LogError("No other disabled equipment strategy");
            return;
        }
        else if (otherEquipmentStrategy == currentEquipmentStrategy)
        {
            Debug.LogError("Other equipment strategy the same with the current one");
            return;
        }

        currentEquipmentStrategy.enabled = false;
        currentEquipmentStrategy = otherEquipmentStrategy;
        currentEquipmentStrategy.enabled = true;
    }

    /// <summary>
    /// Checks new strategy when enabled
    /// </summary>
    public override void EnableStrategy(Strategy newStrategy)
    {
        if (newStrategy is not EquipmentStrategy) // Due to the event being static
            return;

        var newEquipmentStrategy = (EquipmentStrategy)newStrategy;

        if (newEquipmentStrategy == currentEquipmentStrategy) // Avoid Unity start-up calls
            return;

        previousEquipmentStrategy = currentEquipmentStrategy;
        currentEquipmentStrategy = newEquipmentStrategy;

        // Check if change of strategies happened while spawning takes place
        StrategyEnabled();

        // Only for the inspector
        if (previousEquipmentStrategy != null)
            previousEquipmentStrategy.enabled = false;
    }

    public override void DisableStrategy(Strategy strategy)
    {
        if (strategy is not EquipmentStrategy)
            return;

        // Prohibit it - only switching strategies should be allowed because the player should have a role at all times
        if (strategy != previousEquipmentStrategy)
        {
            //Debug.LogWarning("Should not disable all strategies at once - try switching between them instead!");
        }
    }
}