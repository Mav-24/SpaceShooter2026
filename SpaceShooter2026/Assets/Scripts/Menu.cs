using UnityEditor;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameObject pauseScreen;

    private void Update()
    {
        //pausing
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            HandlePause(true);
        } else
        {
            HandlePause(false);
        }
    }

    public static void HandlePause(bool paused)
    {
        if (paused)
        {
            Time.timeScale = 0;
            
        }
    }
}
