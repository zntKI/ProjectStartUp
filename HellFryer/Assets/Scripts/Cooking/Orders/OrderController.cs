using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderController : MonoBehaviour
{
    public System.Action<OrderController> onTimeout;
    
    public Item food;

    public UnityEngine.UI.Image timerUIComponent;

    float totalOrderTime = 30.0f;
    float orderTimeLeft;

    bool hasStarted = false;

    float scoreCoefficient = 200;

    void Update()
    {
        if (!hasStarted)
        {
            return;
        }

        orderTimeLeft -= Time.deltaTime;
        timerUIComponent.fillAmount = orderTimeLeft / totalOrderTime;

        if (orderTimeLeft <= 0.0f)
        {
            timerEnded();
        }
    }

    public void StartTimer()
    {
        hasStarted = true;

        orderTimeLeft = totalOrderTime;
    }

    void OrderTimeOut()
    {
        onTimeout.Invoke(this);
    }

    public void CompleteOrder()
    {
        Destroy(timerUIComponent.transform.parent.gameObject);
        Destroy(gameObject);
    }

    public void SetTimer(float seconds)
    {
        totalOrderTime = seconds;
    }

    public float GetScore()
    {
        float timeLeftPercent = orderTimeLeft / totalOrderTime;

        return timeLeftPercent * scoreCoefficient;
    }

    void timerEnded()
    {
        OrderTimeOut();
    }
}
