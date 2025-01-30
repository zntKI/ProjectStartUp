using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

/// <summary>
/// Does the heavy-lifting of role switching:<br></br>
/// Communicates with the strategy controllers to retrieve current strategy<br></br>
/// </summary>
[RequireComponent(typeof(RoleStrategyController))]
public class RoleController : MonoBehaviour
{
    [SerializeField]
    private Vector3 zoomedOutCameraPos;

    private Vector3 normalCameraPos;
    private CameraFollow cameraController;

    [SerializeField]
    private GameObject cookModel;
    [SerializeField]
    private GameObject hunterModel;

    public GameObject cookBookMini;
    public GameObject hunterBookMini;

    public GameObject cookBookOpen;
    public GameObject hunterBookOpen;

    private RoleStrategyController roleStrategyController;

    private RoleStrategy currentRoleStrategy;

    void Start()
    {
        roleStrategyController = GetComponent<RoleStrategyController>();

        cameraController = transform.parent.GetComponentInChildren<CameraFollow>();
        normalCameraPos = cameraController.offset;

        //roleStrategyController.OnStrategyEnabled += CheckChangingRoleStrategy;

        // Retrieves it from the Start so that when SetupSpawning is called it does not do an early return
        UpdateCurrentRoleStrategy();
    }

    private void UpdateCurrentRoleStrategy()
    {
        currentRoleStrategy = roleStrategyController.CurrentRoleStrategy;
        currentRoleStrategy.UpdateEquipmentType();
    }

    public void SwitchRoles()
    {
        if (roleStrategyController == null) // Because of spawning player before executing Start and OnTriggerEnter called in RoleSwitchAreaController
            roleStrategyController = GetComponent<RoleStrategyController>();


        roleStrategyController.SwitchRoles();
        UpdateCurrentRoleStrategy();

        //SwitchCameraAngle();

        SwitchModels();

        if(cookBookMini != null)
        {
            SwitchBooksMini();
            SwitchBooksOpen();
        }
    }

    private void SwitchCameraAngle()
    {
        if (cameraController == null) // Because of spawning player before executing Start and OnTriggerEnter called in RoleSwitchAreaController
            cameraController = transform.parent.GetComponentInChildren<CameraFollow>();

        if (currentRoleStrategy is CookRoleStrategy)
        {
            cameraController.offset = normalCameraPos;
        }
        else if (currentRoleStrategy is HunterRoleStrategy)
        {
            cameraController.offset = zoomedOutCameraPos;
        }
    }

    private void SwitchModels()
    {
        if (cookModel.activeSelf)
        {
            hunterModel.SetActive(true);
            cookModel.SetActive(false);
        }
        else if (hunterModel.activeSelf)
        {
            cookModel.SetActive(true);
            hunterModel.SetActive(false);
        }
    }

    private void SwitchBooksMini()
    {
        if (cookBookMini.activeSelf)
        {
            hunterBookMini.SetActive(true);
            cookBookMini.SetActive(false);
        }
        else if (hunterBookMini.activeSelf)
        {
            hunterBookMini.SetActive(false);
            cookBookMini.SetActive(true);
        }
    }

    private void SwitchBooksOpen()
    {
        if (cookBookOpen.activeSelf)
        {
            hunterBookOpen.SetActive(true);
            cookBookOpen.SetActive(false);
        }
        else if (hunterBookOpen.activeSelf)
        {
            hunterBookOpen.SetActive(false);
            cookBookOpen.SetActive(true);
        }
    }

    public void PerformTask()
    {
        currentRoleStrategy.PerformTask();
    }

    public bool IsBookOpen()
    {
        return cookBookOpen.activeSelf || hunterBookOpen.activeSelf;
    }

    public void OpenBook()
    {
        if (cookBookMini.activeSelf)
        {
            cookBookOpen.SetActive(true);
        }
        else if (hunterBookMini.activeSelf)
        {
            hunterBookOpen.SetActive(true);
        }
    }

    public void CloseBook()
    {
        if (cookBookMini.activeSelf)
        {
            cookBookOpen.SetActive(false);
        }
        else if (hunterBookMini.activeSelf)
        {
            hunterBookOpen.SetActive(false);
        }
    }

    public void OnPlayerWalkSound()
    {
        if (currentRoleStrategy is CookRoleStrategy)
        {
            SoundManager.instance.PlayCookWalk();
        }
        else
        {
            SoundManager.instance.PlayHunterWalk();
        }
    }

    public void OnPlayerStopWalkSound()
    {
        SoundManager.instance.StopWalk();
    }

    void OnDestroy()
    {
        //roleStrategyController.OnStrategyEnabled -= CheckChangingRoleStrategy;
    }
}