using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject pauseScreen;

    private enum State
    {
        Title,
        Game,
        Pause,
        End
    }

    private static State TITLE = State.Title;
    private static State GAME = State.Game;
    private static State PAUSE = State.Pause;
    private static State END = State.End;

    private void Update()
    {
        //pausing
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            MenuInterface(PAUSE);
        } else
        {
            MenuInterface(PAUSE);
        }
    }

    private void MenuInterface(State state)
    {
        //freezes game
        if (state != GAME)
        {
            Time.timeScale = 0;

            switch (state)
            {
                case State.Title:
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                    break;
                case State.Pause:
                    break;
                case State.End:
                    break;


            }
            
        } else
        {
            Time.timeScale = 1;
        }
    }
}
