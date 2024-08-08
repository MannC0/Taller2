using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreenController : MonoBehaviour
{
    public GameObject deathScreenCanvas;

    private void Start()
    {
        // Ensure the death screen is hidden at the start
        deathScreenCanvas.SetActive(false);
    }

    public void ShowDeathScreen()
    {
        // Show the death screen
        deathScreenCanvas.SetActive(true);

        // Pause the game
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        // Unpause the game
        Time.timeScale = 1f;

        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ReturnToMainMenu()
    {
        // Unpause the game
        Time.timeScale = 1f;

        // Load the main menu scene
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        // Unpause the game
        Time.timeScale = 1f;

        // Quit the application
        Application.Quit();

        // If running in the editor
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
