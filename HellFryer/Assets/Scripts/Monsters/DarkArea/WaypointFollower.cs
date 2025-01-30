using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    public event Action<Transform> OnWaypointChange;

    private List<Transform> waypoints;

    [SerializeField]
    private float speed = 2f;

    private int currentWaypointIndex = 0;

    void Start()
    {
        waypoints = new List<Transform>();

        Transform wingCreatureParent = transform.parent;
        for (int i = 0; i < wingCreatureParent.childCount; i++)
        {
            Transform child = wingCreatureParent.GetChild(i);
            if (child.CompareTag("WingCreatureWaypoint"))
            {
                waypoints.Add(child);
            }
        }
    }

    void Update()
    {
        if (Vector3.Distance(waypoints[currentWaypointIndex].transform.position, transform.position) < .1f)
        {
            currentWaypointIndex++;
            if (currentWaypointIndex >= waypoints.Count)
            {
                currentWaypointIndex = 0;
            }

            OnWaypointChange?.Invoke(waypoints[currentWaypointIndex].transform);
        }
        transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position, Time.deltaTime * speed);
    }
}