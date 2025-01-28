using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Order : MonoBehaviour
{
    itemType food;

    public System.Action onTimeout;

    public void StartOrder()
    {

    }

    void EndOrder()
    {
        onTimeout.Invoke();
    }
}
