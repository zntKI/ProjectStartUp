using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SignMover : MonoBehaviour
{
    [SerializeField] Transform waypoint1;
    [SerializeField] Transform waypoint2;
    [SerializeField] float speed = 1f;

    Transform currentWaypoint;


    private void Start()
    {
        currentWaypoint = waypoint1;
    }

    private void Update()
    {
        if (currentWaypoint == null)
        {
            return;
        }

        MoveTowardsWaypoint();
    }

    void MoveTowardsWaypoint()
    {
        float step = speed * Time.deltaTime;
        Vector3 targetPos = new Vector3(transform.position.x, currentWaypoint.position.y, transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, targetPos, step);

        float margin = 0.5f;
        if (Vector3.Distance(transform.position, targetPos) < margin)
        {
            SwitchCurrentWaypoint();
        }
    }

    void SwitchCurrentWaypoint()
    {
        if(currentWaypoint == waypoint1)
        {
            currentWaypoint = waypoint2;
        }
        else
        {
            currentWaypoint = waypoint1;
        }
    }
}
