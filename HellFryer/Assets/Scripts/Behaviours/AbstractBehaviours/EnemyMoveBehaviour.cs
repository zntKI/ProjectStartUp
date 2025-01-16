using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyMoveBehaviour : MoveBehaviour
{
    [SerializeField] protected float targetRange = 0.2f;
    public Vector3 targetPosition;

    public virtual void SetTargetPosition(Vector3 targetPosition)
    {
        this.targetPosition = targetPosition;
    }

    public abstract bool IsTargetReached();
}
