using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Order : MonoBehaviour
{
    public System.Action<Order> onTimeout;
    
    public itemType food;

    float orderTime = 60.0f;

    bool hasStarted = false;

    public void StartTimer()
    {
        hasStarted = true;
    }

    void EndOrder()
    {
        onTimeout.Invoke(this);
    }

    void Update()
    {
        if (!hasStarted)
        {
            return;
        }

        orderTime -= Time.deltaTime;

        if (orderTime <= 0.0f)
        {
            timerEnded();
        }
    }

    void timerEnded()
    {
        EndOrder();
    }
}
