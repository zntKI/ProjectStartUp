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
    InventorySelector inventorySelector;

    RoleController roleController;

    private Vector2 lookDir;

    void Start()
    {
        mover = GetComponent<PlayerMovement>();
        pickupHandler = GetComponent<PlayerPickupHandler>();
        if (pickupHandler != null)
        {
            pickupHandler.SetPlayerController(this);
        }
        heldItemHandler = GetComponent<PlayerHeldItemHandler>();
        if(heldItemHandler != null)
        {
            heldItemHandler.SetPlayerController(this);
        }

        inventorySelector = GetComponent<InventorySelector>();



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
        if (!context.performed)
        {
            return;
        }

        if (heldItemHandler.PlaceIngredient() == null)
        {
            heldItemHandler.DropHeldItem();
        }
    }

    public void OnHold(CallbackContext context)
    {
        if (context.performed)
        {
            heldItemHandler.HoldSelectedItem();
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

    public void OnSelectLeft(CallbackContext context)
    {
        inventorySelector.OnSelectLeft(context);
    }

    public void OnSelectRight(CallbackContext context)
    {
        inventorySelector.OnSelectRight(context);
    }

    public void OnPerformTask(CallbackContext context)
    {
        if (context.performed)
        {
            roleController.PerformTask();

            Collider[] hitColliders = Physics.OverlapSphere(gameObject.transform.position + gameObject.transform.forward, 1.1f);
            AbstractCookingDevice cookingDevice = heldItemHandler.GetClosestCookingDevice(transform, hitColliders);
            if (cookingDevice == null)
            {
                return;
            }

            cookingDevice.CheckCooking();
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

    public int GetSelectedItemSlot()
    {
        return inventorySelector.selectedSlot;
    }

    
}
