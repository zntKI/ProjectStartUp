using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    private GameObject playerPrefab;
    [SerializeField]
    private Transform player2SelectorContent;
    [SerializeField]
    private GameObject player2HeldItemContainer;

    [SerializeField]
    private GameObject cookBookMini;
    [SerializeField]
    private GameObject hunterBookMini;

    [SerializeField]
    private GameObject cookBookOpen;
    [SerializeField]
    private GameObject hunterBookOpen;

    private PlayerInputManager inputManager;

    // Start is called before the first frame update
    void Start()
    {
        if (playerPrefab != null && player2SelectorContent !=  null) {
            InventorySelector player2Selector = playerPrefab.GetComponentInChildren<InventorySelector>();
            player2Selector.SelectorContent = player2SelectorContent;

            HeldItemDisplay player2HeldItemDisplay = playerPrefab.GetComponentInChildren<HeldItemDisplay>();
            player2HeldItemDisplay.heldItemSlot = player2HeldItemContainer;

            RoleController player2RoleController = playerPrefab.GetComponentInChildren<RoleController>();
            player2RoleController.cookBookMini = cookBookMini;
            player2RoleController.hunterBookMini = hunterBookMini;
            player2RoleController.cookBookOpen = cookBookOpen;
            player2RoleController.hunterBookOpen = hunterBookOpen;

            inputManager = GetComponent<PlayerInputManager>();
            inputManager.playerPrefab = playerPrefab;
            inputManager.JoinPlayer();
        }
        else
        {
            Debug.Log("Missing player 2 information");
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
