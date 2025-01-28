using UnityEngine;
using UnityEngine.SceneManagement;  
using UnityEngine.UI;  

public class PauseMenuController : MonoBehaviour
{
    public GameObject pausePanel; 
    public Button resumeButton; 
    public Button mainMenuButton; 
    public GameObject inventoryPanel; 

    private bool isPaused = false; 

    void Start()
    {
        pausePanel.SetActive(false); 

        resumeButton.onClick.AddListener(ResumeGame);
        mainMenuButton.onClick.AddListener(GoToMainMenu);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0f; 
        pausePanel.SetActive(true); 
        inventoryPanel.SetActive(false); 
        isPaused = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f; 
        pausePanel.SetActive(false); 
        inventoryPanel.SetActive(true); 
        isPaused = false;
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene(0); 
    }
}
