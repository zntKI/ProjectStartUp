using UnityEngine;
using UnityEngine.SceneManagement;  
using UnityEngine.UI;  

public class PauseMenuController : MonoBehaviour
{
    public static PauseMenuController instance { get; private set; }

    public GameObject pausePanel; 
    public Button resumeButton; 
    public Button mainMenuButton; 
    public GameObject inventoryPanel; 
    public GameObject divider; 
    public GameObject bookP1; 
    public GameObject bookP2; 
    public GameObject bookOpenP1; 
    public GameObject bookOpenP2; 
    public GameObject heldItem1; 
    public GameObject heldItem2; 
    public GameObject orders; 

    public bool isPaused = false;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    void Start()
    {
        pausePanel.SetActive(false); 

        resumeButton.onClick.AddListener(ResumeGame);
        mainMenuButton.onClick.AddListener(GoToMainMenu);
    }

    public void OnPause()
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

    public void PauseGame()
    {
        Time.timeScale = 0f; 
        pausePanel.SetActive(true); 
        inventoryPanel.SetActive(false);
        divider.SetActive(false);
        bookP1.SetActive(false);
        bookP2.SetActive(false);
        bookOpenP1.SetActive(false);
        bookOpenP2.SetActive(false);
        heldItem1.SetActive(false); 
        heldItem2.SetActive(false); 
        orders.SetActive(false); 
        isPaused = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f; 
        pausePanel.SetActive(false); 
        inventoryPanel.SetActive(true);
        divider.SetActive(true);
        bookP1.SetActive(true);
        bookP2.SetActive(true);
        bookOpenP1.SetActive(true);
        bookOpenP2.SetActive(true);
        heldItem1.SetActive(true);
        heldItem2.SetActive(true);
        orders.SetActive(true);
        isPaused = false;
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene(0); 
    }
}
