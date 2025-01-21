using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeController : MonoBehaviour
{
    private Rigidbody rb;

    private Transform player;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        player = transform.parent;
    }

    public void LaunchKnife()
    {
        // Outgoing direction: launch with initial directional force that slowly reduces as it approaches the final distance:
        //      distanceToTarget/initialThrowRange = currentDirForce/initialDirForce
        // Incoming direction: constantly update the player's position and increase the directional force accordingly:
        //      distanceToPlayer/distanceToPlayerFromTargetReach = initialDirForce/currentDirForce?

        
    }
}