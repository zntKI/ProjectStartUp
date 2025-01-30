using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignMover : MonoBehaviour
{
    [SerializeField] Transform waypoint1;
    [SerializeField] Transform waypoint2;

    Transform currentWaypoint;

    float moveDist = 1f;

    private void Start()
    {
        currentWaypoint = waypoint1;
    }

    void MoveTowardsWaypoint()
    {
        float step = moveDist * Time.deltaTime;
        Vector3 targetPos = new Vector3(transform.position.x, currentWaypoint.position.y, transform.position.x);
        transform.position = Vector3.MoveTowards(transform.position, targetPos, step);



    }

    void SwitchCurrentWaypoint()
    {

    }
}
