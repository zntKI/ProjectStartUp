using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OrdersManager : MonoBehaviour
{
    public static OrdersManager instance { get; private set; }

    List<Order> orders = new List<Order>();
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    void AddOrder()
    {

    }

    void RemoveOrder()
    {

    }
}
