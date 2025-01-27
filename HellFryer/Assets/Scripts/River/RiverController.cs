using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class RiverController : MonoBehaviour
{
    [SerializeField]
    private float timeBetweenItemDestroy;

    /// <summary>
    /// <playerInRange, timeCounterForGivenPlayer><br></br>
    /// List because I cannot modify value typed values by themselves
    /// </summary>
    private Dictionary<PlayerController, List<float>> currentPlayers;

    void Start()
    {
        currentPlayers = new Dictionary<PlayerController, List<float>>();

        var collider = GetComponent<SphereCollider>();
        collider.radius = transform.parent.GetComponent<PullRange>().Radius * 0.8f;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentPlayers.Count > 0)
        {
            foreach (var player in currentPlayers)
            {
                player.Value[0] += Time.deltaTime;
                if (player.Value[0] > timeBetweenItemDestroy)
                {
                    // TODO Nikola: Destroy an item that is NOT an equipment
                    player.Value[0] = 0f;
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        PlayerController player;
        if (other.transform.TryGetComponent<PlayerController>(out player))
        {
            currentPlayers.Add(player, new List<float>() { 0f });
        }
    }

    void OnTriggerExit(Collider other)
    {
        PlayerController player;
        if (other.transform.TryGetComponent<PlayerController>(out player))
        {
            if (!currentPlayers.Remove(player))
                Debug.LogError("Player not in collection in RiverController and cant be removed!!!");
        }
    }
}
