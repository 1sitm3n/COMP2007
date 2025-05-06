using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("ArenaMain"); //  main game scene
    }

    public void PlayTutorial()
    {
        SceneManager.LoadScene("TutorialScene"); // tutorial scene name - removed. 
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
