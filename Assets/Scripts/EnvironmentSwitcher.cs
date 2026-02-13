using UnityEngine;
using UnityEngine.SceneManagement;

public class EnvironmentSwitcher : MonoBehaviour
{
    // ONLY environment scenes here
    public string[] environmentScenes;

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
        if (Input.GetKeyDown(KeyCode.T))
        {
            currentEnvIndex = (currentEnvIndex + 1) % environmentScenes.Length;
            SceneManager.LoadScene(environmentScenes[currentEnvIndex]);
        }
    }
}
