using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    void Start()
    {
        // Show and unlock cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;


    }

    public void RestartGame()
    {
        SceneManager.LoadScene("CityScene");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game Quit"); // Works only in build
    }
}
