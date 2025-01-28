using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // Singleton instance of GameManager

    void Awake()
    {
        // Singleton pattern to ensure only one instance of GameManager exists
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Keep the GameManager alive across scenes
        }
    }

    void Start()
    {
        // Play background music when the game starts
        SoundManager.instance.PlayBackgroundMusic();
    }

    public void StopBackgroundMusic()
    {
        // Expose a method to stop the background music
        SoundManager.instance.StopBackgroundMusic();
    }
}
