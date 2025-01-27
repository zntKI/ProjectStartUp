using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

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

    private List<PlayerController> players;

    private PlayerController currentPlayer;
    private int currentPlayerIndex;

    private float timeCounter;

    void Start()
    {
        players = new List<PlayerController>();

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
                currentPlayerIndex++;
                if (currentPlayerIndex >= players.Count)
                {
                    currentPlayerIndex = 0;
                }

                currentPlayer = players[currentPlayerIndex];

                LaunchProjectile();
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
            if (players.Count == 0)
            {
                currentPlayerIndex = 0;
                currentPlayer = player;
                state = WingCreatureState.Attacking;

                timeCounter = timeBetweenProjectileLaunch;

                LaunchProjectile();
            }

            players.Add(player);
        }
    }

    void OnTriggerExit(Collider other)
    {
        PlayerController player;
        if (other.transform.TryGetComponent<PlayerController>(out player))
        {
            if (!players.Remove(player))
                Debug.LogError("Player not in collection in WingCreatureController and cant be removed!!!");

            if (players.Count == 0)
            {
                currentPlayer = null;
                state = WingCreatureState.Roaming;
            }
        }
    }

    private void LaunchProjectile()
    {
        ProjectileController projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity).GetComponent<ProjectileController>();
        projectile.Init(currentPlayer.gameObject, GetComponent<SphereCollider>().radius);

        timeCounter = 0f;
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