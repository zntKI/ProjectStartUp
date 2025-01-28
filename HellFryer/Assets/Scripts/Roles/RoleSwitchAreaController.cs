using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleSwitchAreaController : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<RoleController>(out RoleController player))
        {
            player.SwitchRoles();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<RoleController>(out RoleController player))
        {
            player.SwitchRoles();
        }
    }
}
