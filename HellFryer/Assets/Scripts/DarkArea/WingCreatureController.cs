using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WingCreatureController : MonoBehaviour
{
    [SerializeField]
    private float timeBetweenProjectileLaunch;
    [SerializeField]
    private GameObject projectilePrefab;
    [SerializeField]
    private GameObject beefItemPrefab;

    private WingCreatureState state = WingCreatureState.Roaming;

    private WaypointFollower waypointFollower;
    private PlayerController currentPlayer;

    private float timeCounter;

    void Start()
    {
        waypointFollower = GetComponent<WaypointFollower>();

        waypointFollower.OnWaypointChange += CheckForLookDir;

        timeCounter = timeBetweenProjectileLaunch;
    }

    public void Die()
    {
        Destroy(transform.parent.gameObject); // Destroy the whole wing creature prefab
        Instantiate(beefItemPrefab, transform.position, Quaternion.identity);
    }

    void Update()
    {
        if (state == WingCreatureState.Attacking)
        {
            transform.LookAt(currentPlayer.transform); // If player, look at player

            timeCounter += Time.deltaTime;
            if (timeCounter > timeBetweenProjectileLaunch)
            {
                // Launch Projectile
                ProjectileController projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity).GetComponent<ProjectileController>();
                projectile.Init(currentPlayer.gameObject, GetComponent<SphereCollider>().radius);

                timeCounter = 0f;
            }
        }
    }

    /// <summary>
    /// Checks to see if it should look to new waypoint
    /// </summary>
    private void CheckForLookDir(Transform newWaypoint)
    {
        if (state == WingCreatureState.Roaming) // If no player, look at next target
        {
            transform.LookAt(newWaypoint);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        PlayerController player;
        if (other.transform.TryGetComponent<PlayerController>(out player))
        {
            currentPlayer = player;
            state = WingCreatureState.Attacking;
        }
    }

    void OnTriggerExit(Collider other)
    {
        PlayerController player;
        if (other.transform.TryGetComponent<PlayerController>(out player))
        {
            currentPlayer = null;
            state = WingCreatureState.Roaming;

            timeCounter = timeBetweenProjectileLaunch;
        }
    }

    void OnDestroy()
    {
        waypointFollower.OnWaypointChange -= CheckForLookDir;
    }
}

public enum WingCreatureState
{
    Roaming,
    Attacking,
}