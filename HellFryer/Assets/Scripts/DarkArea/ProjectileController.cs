using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField]
    private float moveAmount = 20f;
    private float toDirectionMoveDistance; // The distance to travel after being reflected by the player
    private float toDirectionDistanceTravelled = 0f;

    private ProjectileFlyState state = ProjectileFlyState.ToTarget;

    private GameObject currentTarget;

    private Vector3 moveDir; // unit vector

    // Start is called before the first frame update
    void Start()
    {
        state = ProjectileFlyState.ToTarget;
    }

    public void Init(GameObject target, float toDirectionMoveDistance)
    {
        state = ProjectileFlyState.ToTarget;

        currentTarget = target;
        this.toDirectionMoveDistance = toDirectionMoveDistance;
    }

    public void SwitchMoveDir(Vector3 newMoveDir)
    {
        state = ProjectileFlyState.ToDirection;

        moveDir = newMoveDir;
        toDirectionDistanceTravelled = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case ProjectileFlyState.ToTarget:

                MoveToTarget();

                break;
            case ProjectileFlyState.ToDirection:

                MoveToDirection();

                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Follow-like missle type of movement
    /// </summary>
    private void MoveToTarget()
    {
        Vector3 vectorBetweenProjectileAndTarget = currentTarget.transform.position - transform.position;
        moveDir = vectorBetweenProjectileAndTarget.normalized;

        transform.position += moveDir * moveAmount * Time.deltaTime;
    }

    /// <summary>
    /// Straight movement following only an initial direction
    /// </summary>
    private void MoveToDirection()
    {
        float modMoveAmount = moveAmount * Time.deltaTime;
        transform.position += moveDir * modMoveAmount;

        toDirectionDistanceTravelled += modMoveAmount;
        if (toDirectionDistanceTravelled > toDirectionMoveDistance) // If travelled over range, delete projectile
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == currentTarget) // If hit target
        {
            Destroy(gameObject);
            // TODO Nikola: Destroy an item that is NOT an equipment
        }
        else if (state == ProjectileFlyState.ToDirection &&
            other.CompareTag("WingCreature") && !other.isTrigger) // If hit creature
        {
            Destroy(gameObject);
            other.GetComponent<WingCreatureController>().Die();
        }
    }
}

public enum ProjectileFlyState
{
    ToTarget,
    ToDirection,
}