using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: make it work also for two enemies

public class RiverMonsterController : MonoBehaviour
{
    [SerializeField]
    private float pullAmount = 1f;

    [SerializeField]
    GameObject bloodPrefab;
    [SerializeField]
    GameObject depressedSoulsPrefab;

    private PlayerController currentPlayer;

    void Start()
    {
        // Automatically adjustable
        var collider = GetComponent<SphereCollider>();
        collider.radius = GetComponent<PullRange>().Radius;
    }

    void Update()
    {
        if (currentPlayer != null)
        {
            Vector3 pullVector = (transform.position - currentPlayer.transform.position).normalized * pullAmount;
            currentPlayer.ShouldPull(pullVector);
        }
    }

    public void Die(Vector3 spawnItemDir)
    {
        Destroy(gameObject);

        StopPulling(); // Stop pulling the player

        float distance = GetComponent<SphereCollider>().radius;
        Vector3 spawnPos = transform.position + spawnItemDir * distance;

        Instantiate(bloodPrefab, spawnPos, Quaternion.identity);
        Instantiate(depressedSoulsPrefab, spawnPos, Quaternion.identity);
    }

    void OnTriggerEnter(Collider other)
    {
        other.transform.TryGetComponent<PlayerController>(out currentPlayer);
    }

    void OnTriggerExit(Collider other)
    {
        if (other.transform.TryGetComponent<PlayerController>(out currentPlayer))
        {
            StopPulling();
        }
    }

    private void StopPulling()
    {
        currentPlayer.ShouldPull(Vector3.zero);
        currentPlayer = null;
    }
}
