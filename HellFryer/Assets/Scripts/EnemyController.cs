using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Behaviours")]
    [SerializeField] EnemyMoveBehaviour moveBehaviour;
    [SerializeField] TargetBehavior targetBehavior;

    void Awake()
    {
        moveBehaviour = GetComponent<EnemyMoveBehaviour>();
        targetBehavior = GetComponent<TargetBehavior>();
    }

    void Update()
    {
        SetTargets();

        if (targetBehavior.targets[0] != null)
        {
            moveBehaviour.SetTargetPosition(targetBehavior.targets[0].transform.position);
        }

        if (moveBehaviour.targetPosition != null)
        {
            moveBehaviour.Move();
        }
    }

    public void SetTargets()
    {
        if (targetBehavior != null)
        {
            targetBehavior.SetTargets();
        }
    }
}
