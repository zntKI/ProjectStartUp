using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HostileItemController : MonoBehaviour
{
    HostileItemBehaviour itemBehaviour;

    private void Start()
    {
        itemBehaviour = GetComponent<HostileItemBehaviour>();
    }

    public void Activate()
    {
        if (itemBehaviour == null) {
            return;
        }

        itemBehaviour.Activate();
    }

    public void Deactivate()
    {
        if (itemBehaviour == null)
        {
            return;
        }

        itemBehaviour.Deactivate();
    }
}
