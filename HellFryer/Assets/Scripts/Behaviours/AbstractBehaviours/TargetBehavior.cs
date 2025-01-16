using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TargetBehavior : MonoBehaviour
{
    [Header("Targeting Properties")]
    public float range = 100f;

    [Header("Current Targets")]
    [SerializeField] public List<GameObject> targets = new List<GameObject>();

    public abstract void RemoveTarget(GameObject target);
    public abstract void SetTargets();

    protected bool IsInRange(Vector3 objectPos)
    {
        float dist = Vector3.Magnitude(objectPos - transform.position);
        return dist <= range;
    }
}
