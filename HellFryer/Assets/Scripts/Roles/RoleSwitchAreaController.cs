using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleSwitchAreaController : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerController>(out PlayerController player))
        {
            player.SwitchRoles();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<PlayerController>(out PlayerController player))
        {
            player.SwitchRoles();
        }
    }
}
