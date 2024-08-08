using UnityEngine;
using UnityEngine.SceneManagement; // To manage scenes if needed

public class GameManager : MonoBehaviour
{
    public GameObject victoryCanvas;

    public void EndGame()
    {
        // Show the victory screen
        victoryCanvas.SetActive(true);

        // Pause the game
        Time.timeScale = 0f;
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f; // Resume game time
        SceneManager.LoadScene("MainMenu");
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; // Resume game time
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

}
