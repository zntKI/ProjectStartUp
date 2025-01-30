using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using static UnityEngine.InputSystem.InputAction;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Camera playerCamera;

    PlayerMovement mover;
    bool isHeldByZombie = false;

    PlayerPickupHandler pickupHandler;
    PlayerHeldItemHandler heldItemHandler;
    InventorySelector inventorySelector;

    RoleController roleController;
    AnimationHandler animationHandler;

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
        if (heldItemHandler != null)
        {
            heldItemHandler.SetPlayerController(this);
        }

        inventorySelector = GetComponent<InventorySelector>();

        roleController = GetComponent<RoleController>();
        animationHandler = GetComponent<AnimationHandler>();
    }

    public void SwitchRoles()
    {
        if (roleController == null) // Because of spawning player before executing Start and OnTriggerEnter called in RoleSwitchAreaController
            roleController = GetComponent<RoleController>();
        if (animationHandler == null) // Because of spawning player before executing Start and OnTriggerEnter called in RoleSwitchAreaController
            animationHandler = GetComponent<AnimationHandler>();

        roleController.SwitchRoles();
        animationHandler.UpdateAnimator();
    }

    public void DisableMovement()
    {
        isHeldByZombie = true;
        mover.ResetInputVector();

        SoundManager.instance.StopWalk();
        animationHandler.PlayIdle(heldItemHandler.heldItem);
    }

    public void EnableMovement()
    {
        isHeldByZombie = false;
    }

    public void OnMove(CallbackContext context)
    {
        if (!isHeldByZombie)
        {
            Vector2 value = context.ReadValue<Vector2>();

            mover.SetInputVector(value);

            if (value.x == 0 && value.y == 0)
            {
                SoundManager.instance.StopWalk();
                animationHandler.PlayIdle(heldItemHandler.heldItem);
            }
            else
            {
                SoundManager.instance.PlayCookWalk();
                animationHandler.PlayRun(heldItemHandler.heldItem);
            }
        }
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

        SoundManager.instance.ItemDrop();
        animationHandler.PlayOnItemDrop();

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
            animationHandler.PlayOnItemHold();
        }
    }

    public void OnPause(CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }

        if(PauseMenuController.instance == null)
        {
            return;
        }

        PauseMenuController.instance.OnPause();
    }

    public void OnTurn(CallbackContext context)
    {
        lookDir = context.ReadValue<Vector2>();

        //Mouse turning
        if (context.control.device.name == "Mouse")
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

            InteractWithCookingDevice();
        }
    }

    public void OnOpenBook(CallbackContext context)
    {
        if (!context.performed)
        {
            return;
        }

        if(!roleController.IsBookOpen()){
            roleController.OpenBook();
        }
        else
        {
            roleController.CloseBook();
        }
    }

    public void OnReturnItem(CallbackContext context)
    {
        if (context.performed)
        {

            ItemController heldItem = heldItemHandler.heldItem;
            if (heldItem == null)
            {
                return;
            }

            heldItemHandler.DropHeldItem();
            InventoryManager.instance.PickupItem(heldItem, GetSelectedItemSlot());
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

    void InteractWithCookingDevice()
    {
        Collider[] hitColliders = Physics.OverlapSphere(gameObject.transform.position + gameObject.transform.forward, 1.1f);
        AbstractCookingDevice cookingDevice = heldItemHandler.GetClosestCookingDevice(transform, hitColliders);
        if (cookingDevice == null)
        {
            return;
        }

        cookingDevice.CheckCooking();
    }
}
