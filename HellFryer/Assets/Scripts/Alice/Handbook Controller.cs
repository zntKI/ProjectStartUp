using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class ToggleMenuKeyboard : MonoBehaviour
{
    public GameObject player1Mini; // Assign your menu prefab in the Inspector
    public GameObject player1Menu; // Assign your menu prefab in the Inspector
    bool player1MenuOpened = false;
    bool player1MiniMade = false;
    private GameObject instantiatedMenu1;
    private GameObject instantiatedMini1;


    public GameObject player2Mini; // Assign your menu prefab in the Inspector
    public GameObject player2Menu; // Assign your menu prefab in the Inspector
    bool player2MenuOpened = false;
    bool player2MiniMade = false;
    private GameObject instantiatedMenu2;
    private GameObject instantiatedMini2;

    public GameObject blurBG1;
    public GameObject blurBG2;

    public GameObject parentObject;


    private void Start()
    {
        instantiatedMini1 = Instantiate(player1Mini, parentObject.transform);
        instantiatedMini2 = Instantiate(player2Mini, parentObject.transform);

    }

    void Update()
    {
        if (Input.GetKey(KeyCode.O))
        {
            ToggleMenu1Visibility();
        } else if (Input.GetKeyUp(KeyCode.O))
        {
            ToggleMenu1Invisibility();
        }

        if (Input.GetKey(KeyCode.P))
        {
            ToggleMenu2Visibility();
        }
        else if (Input.GetKeyUp(KeyCode.P))
        {
            ToggleMenu2Invisibility();
        }
    }

    private void ToggleMenu1Visibility()
    {
       if (player1MenuOpened == false)
        {
            SetBlur1(true);

            instantiatedMenu1 = Instantiate(player1Menu, parentObject.transform);
            player1MenuOpened = true;
            Destroy(instantiatedMini1);
        }
    }

    private void ToggleMenu1Invisibility()
    {
            SetBlur1(false);

            //ToggleMiniVisibility();
            Destroy(instantiatedMenu1);
            //instantiatedMenu = Instantiate(player2Mini, transform);
            player1MenuOpened = false;
            instantiatedMini1 = Instantiate(player1Mini, parentObject.transform);


    }

    private void ToggleMenu2Visibility()
    {
        if (player2MenuOpened == false)
        {
            SetBlur2(true);
            instantiatedMenu2 = Instantiate(player2Menu, parentObject.transform);
            player2MenuOpened = true;
            Destroy(instantiatedMini2);
        }

    }

    private void ToggleMenu2Invisibility()
    {
            SetBlur2(false);

            //ToggleMiniVisibility();
            Destroy(instantiatedMenu2);
            //instantiatedMenu = Instantiate(player2Mini, transform);
            player2MenuOpened = false;
        instantiatedMini2 = Instantiate(player2Mini, parentObject.transform);

    }
    void SetBlur1(bool enable)
    {
        if (blurBG1 != null)
        {
            blurBG1.SetActive(enable);
        }
    }

    void SetBlur2(bool enable)
    {
        if (blurBG2 != null)
        {
            blurBG2.SetActive(enable);
        }
    }

}
