using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullRangeController : MonoBehaviour
{
    [SerializeField]
    private float pullAmount = 1f;

    private PlayerController currentPlayer;

    void Start()
    {
        // Automatically adjustable
        var collider = GetComponent<SphereCollider>();
        collider.radius = transform.parent.GetComponent<PullRange>().Radius;
    }

    void Update()
    {
        if (currentPlayer != null)
        {
            Vector3 pullVector = (transform.position - currentPlayer.transform.position).normalized * pullAmount;
            currentPlayer.ShouldPull(pullVector);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        PlayerController player;
        if (other.transform.TryGetComponent<PlayerController>(out player))
        {
            currentPlayer = player;
        }
    }

    void OnTriggerExit(Collider other)
    {
        PlayerController player;
        if (other.transform.TryGetComponent<PlayerController>(out player))
        {
            StopPulling();
        }
    }

    public void StopPulling()
    {
        currentPlayer.ShouldPull(Vector3.zero);
        currentPlayer = null;
    }
}
