using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class OrdersManager : MonoBehaviour
{
    public static OrdersManager instance { get; private set; }

    [Header("Required Components")]
    [SerializeField] OrderCounterController orderCounterController;
    [SerializeField] Transform orderUIContent;
    [SerializeField] OrderController orderControllerPrefab;
    [SerializeField] GameObject orderUIItemPrefab;

    [Header("List of possible orders")]
    [SerializeField] List<Item> cookedFoods = new List<Item>();

    [Header("Current orders")]
    [SerializeField] List<OrderController> orders = new List<OrderController>();

    int maxOrderCount = 5;
    int currentOrderCount = 0;

    float orderInterval = 3.0f;

    float totalScore = 0;

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
        InvokeRepeating(nameof(AddRandomOrder), 0, orderInterval);
    }

    public bool AreOrdersOver()
    {
        return currentOrderCount >= maxOrderCount;
    }

    void AddRandomOrder()
    {
        if (currentOrderCount >= maxOrderCount)
        {
            CancelInvoke(nameof(AddRandomOrder));
            return;
        }

        if (cookedFoods.Count == 0) { return; }

        int randIndex = Random.Range(0, cookedFoods.Count);
        int randOrderDuration = Random.Range(30, 60);

        AddOrder(cookedFoods[randIndex], randOrderDuration);

        currentOrderCount++;
    }

    void AddOrder(Item food, float orderDuration)
    {
        GameObject orderUI = Instantiate(orderUIItemPrefab, orderUIContent);
        UnityEngine.UI.Image orderIcon = orderUI.transform.Find("OrderIcon").GetComponent<UnityEngine.UI.Image>();
        orderIcon.sprite = food.icon;

        OrderController order = Instantiate(orderControllerPrefab, transform);
        order.food = food;
        order.SetTimer(orderDuration);

        UnityEngine.UI.Image orderTimer = orderUI.transform.Find("OrderTimer").GetComponent<UnityEngine.UI.Image>();

        order.timerUIComponent = orderTimer;
        order.StartTimer();
        orders.Add(order);

        order.onTimeout += RemoveOrder;
    }

    void RemoveOrder(OrderController order)
    {
        order.onTimeout -= RemoveOrder;
        totalScore += order.GetScore();
        order.CompleteOrder();
        orders.Remove(order);
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
        foreach (OrderController order in orders)
        {
            if (orderCounterController.CompareFood(order.food.itemType))
            {
                RemoveOrder(order);
                orderCounterController.removeIngredientsFromContainers();
                Debug.Log(totalScore);
                return;
            }
        }

        Debug.Log("Wrong order");
        orderCounterController.removeIngredientsFromContainers();
    }


}
