using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BasicEnemyMoveBehaviour : EnemyMoveBehaviour
{
    Vector3 moveDirection;
    float moveSpeed = 1;

    private void Start()
    {
        moveDirection = new Vector3(transform.position.x - targetPosition.x, 0, transform.position.y - targetPosition.y);
    }

    private void FixedUpdate()
    {
        moveDirection = new Vector3(transform.position.x - targetPosition.x, 0, transform.position.y - targetPosition.y);
    }

    public override bool IsTargetReached()
    {
        float dist = moveDirection.magnitude;

        if (dist < targetRange)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public override void Move()
    {
        if (!IsTargetReached())
        {
            var step = moveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
        }
    }
}
