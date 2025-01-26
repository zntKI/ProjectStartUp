using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    public event Action<Transform> OnWaypointChange;

    [SerializeField]
    private GameObject[] waypoints;

    [SerializeField]
    private float speed = 2f;

    private int currentWaypointIndex = 0;

    void Update()
    {
        if (Vector3.Distance(waypoints[currentWaypointIndex].transform.position, transform.position) < .1f)
        {
            currentWaypointIndex++;
            if (currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = 0;
            }

            OnWaypointChange?.Invoke(waypoints[currentWaypointIndex].transform);
        }
        transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position, Time.deltaTime * speed);
    }
}