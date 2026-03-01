using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class MainManager : MonoBehaviour
{
    public static MainManager mainManager;
    public List<string> questNames = new();

    private void Awake()
    {
        if (mainManager != null)
        {
            Destroy(gameObject);
        }
        mainManager = this;
        DontDestroyOnLoad(gameObject);
    }
}
