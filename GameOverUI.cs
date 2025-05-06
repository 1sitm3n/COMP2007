using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    public GameObject gameOverPanel;

    void Update()
    {
        if (gameOverPanel.activeSelf && Input.GetKeyDown(KeyCode.R))
        {
            // Unpause before restarting
            Time.timeScale = 1f;

            // Reload scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void ShowGameOver()
    {
        gameOverPanel.SetActive(true);

        // Use delay or animation fade before freezing if needed
        Time.timeScale = 0f;
    }
}
