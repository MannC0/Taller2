using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TMP_Text victoryScoreText;
    public GameObject victoryCanvas;
    private PlayerMovement playerMovement;

    private void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        victoryCanvas.SetActive(false);
    }

    public void EndGame()
    {

        // Ensure the PlayerMovement script is found
        if (playerMovement == null)
        {
            playerMovement = FindObjectOfType<PlayerMovement>();
        }

        // Update the victory screen score text with the player's final score
        if (victoryScoreText != null && playerMovement != null)
        {
            victoryScoreText.text = "Final Score: " + playerMovement.dinero.ToString();
        }

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

    public void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

}
