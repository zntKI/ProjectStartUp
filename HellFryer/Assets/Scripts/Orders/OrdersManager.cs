using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OrdersManager : MonoBehaviour
{
    public static OrdersManager instance { get; private set; }

    [SerializeField] OrderCounterController orderCounterController;

    [SerializeField] List<Order> orders = new List<Order>();

    Order sampleOrder;

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

    private void Start()
    {
        sampleOrder = GetComponent<Order>();

        AddOrder(itemType.Spaghetti);
    }

    void SpawnOrders()
    {
        //Add new order in intervals
    }
    void AddOrder(itemType food)
    {
        Order order = Instantiate(sampleOrder);
        order.food = food;
        order.StartTimer();
        orders.Add(order);

        order.onTimeout += RemoveOrder;
    }

    void RemoveOrder(Order order)
    {
        orders.Remove(order);
        Destroy(order);
    }

    private void Update()
    {
        if (!orderCounterController.AreAllIngredientsPlaced())
        {
            return;
        }

        CheckOrders();
    }

    void CheckOrders()
    {
        foreach (Order order in orders)
        {
            if (orderCounterController.CompareFood(order.food))
            {
                Debug.Log("Completed order");
                RemoveOrder(order);
                orderCounterController.removeIngredientsFromContainers();
                break;
            }
        }

        Debug.Log("Wrong order");
        orderCounterController.removeIngredientsFromContainers();
    }
}
