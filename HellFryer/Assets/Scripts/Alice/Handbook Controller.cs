using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HandbookController : MonoBehaviour
{
    public static HandbookController instance { get; private set; }

    public GameObject player1Mini; // Assign your menu prefab in the Inspector
    public GameObject player1Menu; // Assign your menu prefab in the Inspector
    bool player1MenuOpened = false;
    //bool player1MiniMade = false;
    private GameObject instantiatedMenu1;
    private GameObject instantiatedMini1;


    public GameObject player2Mini; // Assign your menu prefab in the Inspector
    public GameObject player2Menu; // Assign your menu prefab in the Inspector
    bool player2MenuOpened = false;
    //bool player2MiniMade = false;
    private GameObject instantiatedMenu2;
    private GameObject instantiatedMini2;

    public GameObject parentObject;

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
        instantiatedMini1 = Instantiate(player1Mini, parentObject.transform);
        instantiatedMini2 = Instantiate(player2Mini, parentObject.transform);
    }

    //void Update()
    //{
    //    if (Input.GetKey(KeyCode.O))
    //    {
    //        ToggleMenu1Visibility();
    //    }
    //    else if (Input.GetKeyUp(KeyCode.O))
    //    {
    //        ToggleMenu1Invisibility();
    //    }

    //    if (Input.GetKey(KeyCode.P))
    //    {
    //        ToggleMenu2Visibility();
    //    }
    //    else if (Input.GetKeyUp(KeyCode.P))
    //    {
    //        ToggleMenu2Invisibility();
    //    }
    //}

    public void ToggleMenu1Visibility()
    {
       if (player1MenuOpened == false)
       {
            //ToggleMenu2Visibility();
            instantiatedMenu1 = Instantiate(player1Menu, parentObject.transform);
            player1MenuOpened = true;
            Destroy(instantiatedMini1);
       }
    }

    public void ToggleMenu1Invisibility()
    {
            //ToggleMiniVisibility();
            Destroy(instantiatedMenu1);
            //instantiatedMenu = Instantiate(player2Mini, transform);
            player1MenuOpened = false;
            instantiatedMini1 = Instantiate(player1Mini, parentObject.transform);
    }

    public void ToggleMenu2Visibility()
    {
        if (player2MenuOpened == false)
        {
            //ToggleMenu2Visibility();
            instantiatedMenu2 = Instantiate(player2Menu, parentObject.transform);
            player2MenuOpened = true;
            Destroy(instantiatedMini2);
        }

    }

    public void ToggleMenu2Invisibility()
    {
            //ToggleMiniVisibility();
            Destroy(instantiatedMenu2);
            //instantiatedMenu = Instantiate(player2Mini, transform);
            player2MenuOpened = false;
            instantiatedMini2 = Instantiate(player2Mini, parentObject.transform);
    }

}
