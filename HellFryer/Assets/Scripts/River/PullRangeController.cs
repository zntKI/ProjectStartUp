using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullRangeController : MonoBehaviour
{
    [SerializeField]
    private float pullAmount = 1f;

    private List<PlayerController> currentPlayers;

    void Start()
    {
        currentPlayers = new List<PlayerController>();

        // Automatically adjustable
        var collider = GetComponent<SphereCollider>();
        collider.radius = transform.parent.GetComponent<PullRange>().Radius;
    }

    void Update()
    {
        if (currentPlayers.Count > 0)
        {
            foreach (var player in currentPlayers)
            {
                Vector3 pullVector = (transform.position - player.transform.position).normalized * pullAmount;
                player.ShouldPull(pullVector);
            }

            SoundManager.instance.PlayRiverMonsterPullingSound();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        PlayerController player;
        if (other.transform.TryGetComponent<PlayerController>(out player))
        {
            currentPlayers.Add(player);
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
        PlayerController player = currentPlayers.Find(p => p == playerToStopPulling);

        player.ShouldPull(Vector3.zero);
        currentPlayers.Remove(player);

        if (currentPlayers.Count == 0)
        {
            SoundManager.instance.StopRiverMonsterPullingSound();
        }
    }

    /// <summary>
    /// Called when the monster dies and should stop pulling all players
    /// </summary>
    public void StopPulling()
    {
        foreach (var player in currentPlayers)
        {
            player.ShouldPull(Vector3.zero);
        }
        currentPlayers.Clear();

        SoundManager.instance.StopRiverMonsterPullingSound();
    }
}
