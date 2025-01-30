using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BloodSplatter : MonoBehaviour
{
    [SerializeField] float dynamicSlipperiness = 0.6f;
    [SerializeField] float staticSlipperiness = 0.3f;

    Vector3 previousVelocity = Vector3.zero;

    GameObject curPlayer;
    void OnTriggerStay(Collider other)
    {
        curPlayer = other.gameObject;

        PlayerController playerController = curPlayer.GetComponent<PlayerController>();

        if (playerController != null)
        {
            Rigidbody rb = curPlayer.GetComponent<Rigidbody>();

            Vector3 newVelocity = rb.velocity * dynamicSlipperiness;
            newVelocity = new Vector3(newVelocity.x, 0, newVelocity.z);
            Vector3 slipperyVector = Vector3.Lerp(previousVelocity, newVelocity, staticSlipperiness);

            playerController.ShouldPull(slipperyVector);

            previousVelocity = slipperyVector;
        }
    }

    void OnTriggerExit(Collider other)
    {
        PlayerController player = other.transform.GetComponent<PlayerController>();

        if (player != null)
        {
            player.ShouldPull(Vector3.zero);
            previousVelocity = Vector3.zero;
            curPlayer = null;
        }
    }

    private void OnDestroy()
    {
        if (curPlayer == null)
        {
            return;
        }

        PlayerController playerController = curPlayer.transform.GetComponent<PlayerController>();

        if (playerController != null)
        {
            playerController.ShouldPull(Vector3.zero);
            previousVelocity = Vector3.zero;
        }
    }

}
