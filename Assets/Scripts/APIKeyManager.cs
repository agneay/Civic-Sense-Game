using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class APIKeyManager : MonoBehaviour
{
    public static APIKeyManager Instance;

    [Header("UI References")]
    public TMP_InputField apiInput;

    public string API_KEY;

    void Awake()
    {
        // Singleton setup
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Check if API key already exists
        if (PlayerPrefs.HasKey("USER_API_KEY"))
        {
            API_KEY = PlayerPrefs.GetString("USER_API_KEY");
            LoadMainMenu();
        }
    }

    public void SaveAPIKey()
    {
        string key = apiInput.text.Trim();

        if (string.IsNullOrEmpty(key))
        {
            Debug.LogWarning("API Key cannot be empty!");
            return;
        }

        PlayerPrefs.SetString("USER_API_KEY", key);
        PlayerPrefs.Save();

        API_KEY = key;

        LoadMainMenu();
    }

    void LoadMainMenu()
    {
        SceneManager.LoadScene("CityScene");
    }
}