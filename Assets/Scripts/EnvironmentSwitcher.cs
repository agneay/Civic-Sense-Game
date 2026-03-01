using UnityEngine;
using UnityEngine.SceneManagement;

public class EnvironmentSwitcher : MonoBehaviour
{
    [Header("Environment Scenes")]
    public string[] environmentScenes;

    [Header("Player Reference")]
    public PlayerMovement playerMovement;

    private int currentEnvIndex = 0;

    void Start()
    {
        string activeScene = SceneManager.GetActiveScene().name;

        for (int i = 0; i < environmentScenes.Length; i++)
        {
            if (environmentScenes[i] == activeScene)
            {
                currentEnvIndex = i;
                break;
            }
        }
    }

    void Update()
    {
        // 🚫 Do nothing if dialogue (or any lock) is active
        if (playerMovement != null && !playerMovement.canControl)
            return;

        if (Input.GetKeyDown(KeyCode.T))
        {
            if (environmentScenes.Length == 0) return;

            currentEnvIndex = (currentEnvIndex + 1) % environmentScenes.Length;
            SceneManager.LoadScene(environmentScenes[currentEnvIndex]);
        }
    }
}