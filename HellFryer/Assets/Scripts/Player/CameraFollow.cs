using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Player follow camera behaviour
/// Code from: https://www.youtube.com/watch?v=g_s0y5yFxYg
/// </summary>
public class CameraFollow : MonoBehaviour
{
    [Header("Follow Parameters")]

    [SerializeField]
    private Transform target = null;

    [SerializeField, Range(0.01f, 1f)]
    private float smoothSpeed = 0.125f;

    [SerializeField]
    public Vector3 offset = new Vector3(0f, 2.25f, -1.5f);

    // For smooth damp function
    private Vector3 velocity = Vector3.zero;

    /// <summary>
    /// Late update runs after all the other updates, this is to take into account new player movement
    /// </summary>
    private void LateUpdate()
    {
    }

    void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        // Do some magic
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);
    }
}
