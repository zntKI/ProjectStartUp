using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ClosestTargetingBehaviour : TargetBehavior
{
    private void Start()
    {
        targets.Insert(0, null);
    }


    public override void RemoveTarget(GameObject target)
    {
        targets.Remove(target);
    }

    public override void SetTargets()
    {
        GameObject closestTarget = GetClosestTarget();
        UpdateTarget(closestTarget);
    }

    GameObject GetClosestTarget()
    {
        GameObject closestTarget = null;
        float closestDistance = Mathf.Infinity;
        foreach (GameObject target in GameObject.FindGameObjectsWithTag("Player"))
        {
            float dist = Vector3.Magnitude(target.transform.position - transform.position);
            if (dist < closestDistance && IsInRange(target.transform.position))
            {
                closestDistance = dist;
                closestTarget = target;
            }
        }

        return closestTarget;
    }

    void UpdateTarget(GameObject target)
    {
        targets[0] = target;
    }
}
