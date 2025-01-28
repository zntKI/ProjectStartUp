using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WingsHostileItemBehaviour : HostileItemBehaviour
{
    [SerializeField, Tooltip("given time to wait between pushing the player away")]
    private float timeToWaitBetweenPushing;
    [SerializeField, Tooltip("given time to perform pushing the player away")]
    private float timeForPushing;

    [SerializeField]
    private float pushAmount = 1f;

    private WingsHostileState state;

    private float timeCounter = 0f;

    private SphereCollider coll;
    private List<PlayerController> currentPlayersInRange;

    void Start()
    {
        state = WingsHostileState.None;

        currentPlayersInRange = new List<PlayerController>();

        coll = GetComponent<SphereCollider>();
        coll.enabled = false; // Enable/Disable due to pushing away functionality
    }

    void Update()
    {
        switch (state)
        {
            case WingsHostileState.None:
                break;
            case WingsHostileState.WaitingStatic:

                timeCounter += Time.deltaTime;
                if (timeCounter > timeToWaitBetweenPushing)
                {
                    SwitchState(WingsHostileState.PushingAway);
                }

                break;
            case WingsHostileState.PushingAway:

                if (currentPlayersInRange.Count > 0)
                {
                    foreach (var player in currentPlayersInRange)
                    {
                        Vector3 pullVector = (player.transform.position - transform.position).normalized * pushAmount;
                        player.ShouldPull(pullVector);
                    }
                }

                timeCounter += Time.deltaTime;
                if (timeCounter > timeForPushing)
                {
                    SwitchState(WingsHostileState.WaitingStatic);
                }

                break;
            default:
                break;
        }
    }

    public void SwitchState(WingsHostileState newState)
    {
        timeCounter = 0f;

        switch (newState)
        {
            case WingsHostileState.None:
                break;
            case WingsHostileState.WaitingStatic:

                coll.enabled = false;
                if (currentPlayersInRange.Count > 0)
                {
                    foreach (var player in currentPlayersInRange)
                    {
                        player.ShouldPull(Vector3.zero);
                    }
                    currentPlayersInRange.Clear();
                }

                break;
            case WingsHostileState.PushingAway:

                coll.enabled = true;

                break;
            default:
                break;
        }

        state = newState;
    }

    public override void Activate()
    {
        SwitchState(WingsHostileState.WaitingStatic);
    }

    public override void Deactivate()
    {
        SwitchState(WingsHostileState.None);
    }

    void OnTriggerEnter(Collider other)
    {
        PlayerController player;
        if (other.transform.TryGetComponent<PlayerController>(out player))
        {
            currentPlayersInRange.Add(player);
        }
    }

    void OnTriggerExit(Collider other)
    {
        PlayerController player;
        if (other.transform.TryGetComponent<PlayerController>(out player))
        {
            StopPulling(player);
        }
    }

    /// <summary>
    /// Called when an individual player goes out of gravity pull range
    /// </summary>
    private void StopPulling(PlayerController playerToStopPulling)
    {
        PlayerController player = currentPlayersInRange.Find(p => p == playerToStopPulling);

        player.ShouldPull(Vector3.zero);
        currentPlayersInRange.Remove(player);
    }
}

public enum WingsHostileState
{
    None,
    WaitingStatic,
    PushingAway,
}