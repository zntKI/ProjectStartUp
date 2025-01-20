using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;
using static UnityEngine.InputSystem.InputAction;

public class PlayerController : MonoBehaviour
{
    PlayerMovement mover;
    PlayerPickupHandler pickupHandler;
    PlayerHeldItemHandler heldItemHandler;

    void Start()
    {
        mover = GetComponent<PlayerMovement>();
        pickupHandler = GetComponent<PlayerPickupHandler>();
        heldItemHandler = GetComponent<PlayerHeldItemHandler>();
    }

    public void OnMove(CallbackContext context)
    {
        mover.SetInputVector(context.ReadValue<Vector2>());
    }

    public void OnPickUp(CallbackContext context)
    {
        if (context.performed)
        {
            pickupHandler.PickupItem();
        }
    }

    public void OnDrop(CallbackContext context)
    {
        if (context.performed)
        {
            heldItemHandler.DropHeldItem(gameObject);
        }
    }

    public void OnHold(CallbackContext context)
    {
        if (context.performed)
        {
            heldItemHandler.HoldSelectedItem(gameObject);
        }
    }
}
