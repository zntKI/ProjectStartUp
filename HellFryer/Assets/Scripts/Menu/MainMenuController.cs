using UnityEngine;
using UnityEngine.SceneManagement; 
using UnityEngine.UI; 

public class MainMenuController : MonoBehaviour
{
    public GameObject controlsPanel; 
    public Image controlsImage; 

    void Start()
    {
        controlsPanel.SetActive(false); 
    }

    // Function for Play Button
    public void OnPlayButtonClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); 
    }

    // Function for Controls Button
    public void OnControlsButtonClicked()
    {
        Debug.Log("Controls button clicked!");
        controlsPanel.SetActive(true); 
    }

 
    public void OnQuitButtonClicked()

    {
        Debug.Log("Quit button clicked!");
        Application.Quit(); 
    }

 
    public void CloseControls()
    {
        controlsPanel.SetActive(false);
    }
}
