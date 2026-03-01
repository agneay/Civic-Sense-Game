using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Camera cam1;
    public Camera cam2;

    void Start()
    {
        cam1.enabled = true;
        cam2.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            SwitchCamera();
        }
    }

    void SwitchCamera()
    {
        cam1.enabled = !cam1.enabled;
        cam2.enabled = !cam2.enabled;
    }
}
