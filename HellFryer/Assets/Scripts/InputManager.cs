using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public Transform player2SelectorContent;
    private PlayerInputManager inputManager;

    // Start is called before the first frame update
    void Start()
    {
        if (playerPrefab != null && player2SelectorContent !=  null) {
            InventorySelector player2Selector = playerPrefab.GetComponentInChildren<InventorySelector>();
            player2Selector.SelectorContent = player2SelectorContent;

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
