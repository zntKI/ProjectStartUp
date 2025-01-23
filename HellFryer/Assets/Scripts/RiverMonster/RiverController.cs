using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiverController : MonoBehaviour
{
    [SerializeField]
    private float timeBetweenItemDestroy;
    private float timeCounter;

    private PlayerController currentPlayer;

    void Start()
    {
        var collider = GetComponent<SphereCollider>();
        collider.radius = transform.parent.GetComponent<PullRange>().Radius * 0.8f;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentPlayer != null)
        {
            timeCounter += Time.deltaTime;
            if (timeCounter > timeBetweenItemDestroy)
            {
                // TODO Nikola: Destroy an item that is NOT an equipment
                timeCounter = 0;
            }
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
        if (other.transform.TryGetComponent<PlayerController>(out currentPlayer))
        {
            currentPlayer = null;
            timeCounter = 0;
        }
    }
}
