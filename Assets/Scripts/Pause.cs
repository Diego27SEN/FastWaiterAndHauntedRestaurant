using UnityEngine;

public class Pause : MonoBehaviour
{
    void Update()
    {
        // presiona ESC
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameManager.Instance.state == GameManager.GameState.Playing)
            {
                GameManager.Instance.PauseGame();
            }
            else if (GameManager.Instance.state == GameManager.GameState.Paused)
            {
                GameManager.Instance.ResumeGame();
            }
        }
    }
}