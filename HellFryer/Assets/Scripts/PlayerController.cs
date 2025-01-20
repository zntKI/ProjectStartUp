using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class PlayerController : MonoBehaviour
{
    PlayerMovement mover;

    private Vector2 lookDir;

    void Start()
    {
        mover = GetComponent<PlayerMovement>();
    }

    public void OnMove(CallbackContext context)
    {
        mover.SetInputVector(context.ReadValue<Vector2>());
    }

    public void OnTurn(CallbackContext context)
    {
        lookDir = context.ReadValue<Vector2>();

        // TODO: Move elsewhere:
        if (lookDir.magnitude > 0.1f)
            transform.LookAt(transform.position + new Vector3(lookDir.x, 0f, lookDir.y));
    }

    public void ShouldPull(Vector3 pullVector)
    {
        mover.SetModifiableVector(pullVector);
    }
}
