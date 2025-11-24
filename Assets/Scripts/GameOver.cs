using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{

    public GameObject gameOver;

    public void MostrarGameOver()
    {
        gameOver.SetActive(true);

    }

    public void BacktoMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}