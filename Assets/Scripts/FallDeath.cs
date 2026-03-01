using UnityEngine;
using UnityEngine.SceneManagement;

public class FallDeath : MonoBehaviour
{
    public float deathY = -95f; // Adjust based on your level

    void Update()
    {
        if (transform.position.y < deathY)
        {
            SceneManager.LoadScene("GameOverScene");
        }
    }
}
