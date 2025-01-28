using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BeefHostileItemBehaviour : HostileItemBehaviour
{
    [SerializeField, Tooltip("given time to wait before starting to roam")]
    private float timeToWait;
    [SerializeField]
    private float waypointFollowSpeed = 2f;

    private BeefHostileState state;

    private float timeCounter = 0f;

    private GameObject[] waypointsToFollow;
    private int currentWaypointIndex = 0;

    void Start()
    {
        waypointsToFollow = GameObject.FindGameObjectsWithTag("BeefWaypoint")
            .OrderBy(w => w.GetComponent<IDHolder>().ID).ToArray();

        state = BeefHostileState.None;
    }

    void Update()
    {
        switch (state)
        {
            case BeefHostileState.None:
                break;
            case BeefHostileState.WaitingStatic:

                timeCounter += Time.deltaTime;
                if (timeCounter > timeToWait)
                {
                    state = BeefHostileState.Roaming;
                }

                break;
            case BeefHostileState.Roaming:

                FollowWaypoints();

                break;
            default:
                break;
        }
    }

    private void FollowWaypoints()
    {
        if(waypointsToFollow.Length == 0)
        {
            return;
        }

        if (Vector3.Distance(waypointsToFollow[currentWaypointIndex].transform.position, transform.position) < .1f)
        {
            currentWaypointIndex++;
            if (currentWaypointIndex >= waypointsToFollow.Length)
            {
                currentWaypointIndex = 0;
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, waypointsToFollow[currentWaypointIndex].transform.position, Time.deltaTime * waypointFollowSpeed);
    }

    public override void Activate()
    {
        state = BeefHostileState.WaitingStatic;
        timeCounter = 0f;
        currentWaypointIndex = 0;
    }

    public override void Deactivate()
    {
        state = BeefHostileState.None;
    }
}

public enum BeefHostileState
{
    None,
    WaitingStatic,
    Roaming,
}