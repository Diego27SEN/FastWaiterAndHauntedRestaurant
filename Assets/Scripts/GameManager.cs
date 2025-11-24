using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour

{
    public enum GameState { None, Start, Playing, Paused, Win, Lose }
    public GameState state = GameState.Start;

    public static GameManager Instance;
    public DialogSystemUI dialogSystemUI;
    public GameObject mainMenu;
    public GameObject gameUI;
    public GameObject pauseMenu;
    public GameObject winScreen;
    public GameObject loseScreen;

    void Start()
    {

    }

    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        SceneManager.sceneLoaded += OnSceneLoaded;

    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        mainMenu = GameObject.Find("MainMenu");
        gameUI = GameObject.Find("GameUI");
        pauseMenu = GameObject.Find("PauseMenu");
        winScreen = GameObject.Find("WinScreen");
        loseScreen = GameObject.Find("LoseScreen");

        UpdateGameState();
    }
    public void SetGameState(GameState newState)
    {
        state = newState;
        UpdateGameState();
    }
    private void UpdateGameState()
    {
        HideAll();

        switch (state)
        {
            case GameState.Start:
                Time.timeScale = 1f;
                if (mainMenu) mainMenu.SetActive(true);
                break;

            case GameState.Playing:
                Time.timeScale = 1f;
                if (gameUI) gameUI.SetActive(true);
                break;

            case GameState.Paused:
                Time.timeScale = 0f;
                if (pauseMenu) pauseMenu.SetActive(true);
                break;

            case GameState.Win:
                Time.timeScale = 0f;
                if (winScreen) winScreen.SetActive(true);
                break;

            case GameState.Lose:
                Time.timeScale = 0f;
                if (loseScreen) loseScreen.SetActive(true);
                break;
        }
    }
    void HideAll()
    {
        if (mainMenu) mainMenu.SetActive(false);
        if (gameUI) gameUI.SetActive(false);
        if (pauseMenu) pauseMenu.SetActive(false);
        if (winScreen) winScreen.SetActive(false);
        if (loseScreen) loseScreen.SetActive(false);
    }

    public void StartGame()
    {
        LoadScene("Restaurante");
    }

    public void PauseGame()
    {
        SetGameState(GameState.Paused);
    }

    public void ResumeGame()
    {
        SetGameState(GameState.Playing);
    }

    public void WinGame()
    {
        SetGameState(GameState.Win);
    }

    public void LoseGame()
    {
        SetGameState(GameState.Lose);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);


    }
}