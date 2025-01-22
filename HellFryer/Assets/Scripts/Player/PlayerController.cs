using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.WSA;
using static UnityEngine.InputSystem.InputAction;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Camera playerCamera;
    PlayerMovement mover;
    PlayerPickupHandler pickupHandler;
    PlayerHeldItemHandler heldItemHandler;

    RoleController roleController;

    private Vector2 lookDir;

    void Start()
    {
        mover = GetComponent<PlayerMovement>();
        pickupHandler = GetComponent<PlayerPickupHandler>();
        heldItemHandler = GetComponent<PlayerHeldItemHandler>();

        roleController = GetComponent<RoleController>();
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

    public void OnTurn(CallbackContext context)
    {
        lookDir = context.ReadValue<Vector2>();
        
        //Mouse turning
        if(context.control.device.name == "Mouse")
        {
            Ray ray = playerCamera.ScreenPointToRay(new Vector3(lookDir.x, lookDir.y, 0));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, LayerMask.GetMask("Ground")))
            {
                lookDir.x = hit.point.x;
                lookDir.y = hit.point.z;

                transform.LookAt(new Vector3(lookDir.x, transform.position.y, lookDir.y));
            }
        }
        else //Controller turning
        {
            // TODO: Move elsewhere:
            if (lookDir.magnitude > 0.1f)
                transform.LookAt(transform.position + new Vector3(lookDir.x, 0f, lookDir.y));
        }
        

        
    }

    public void OnPerformTask(CallbackContext context)
    {
        if (context.performed)
        {
            roleController.PerformTask();
        }
    }

    public void OnOpenBook(CallbackContext context)
    {
        if (context.performed)
        {
            roleController.OpenBook();
        }
    }

    public void ShouldPull(Vector3 pullVector)
    {
        mover.SetModifiableVector(pullVector);
    }
}
