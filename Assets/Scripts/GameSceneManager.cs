using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class GameSceneManager : MonoBehaviour
{
    public static GameSceneManager Instance { get; private set; }

    public enum SceneType
    {
        MainMenu,
        Game,
        End
    }

    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private float minimumLoadTime = 0.5f;

    private Dictionary<SceneType, string> sceneNames;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeSceneNames();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeSceneNames()
    {
        sceneNames = new Dictionary<SceneType, string>
        {
            { SceneType.MainMenu, "MainMenu" },
            { SceneType.Game, "Game" },
            { SceneType.End, "End" }
        };
    }

    public void LoadScene(SceneType sceneType)
    {
        StartCoroutine(LoadSceneAsync(sceneType));
    }

    private IEnumerator LoadSceneAsync(SceneType sceneType)
    {
        if (loadingScreen != null)
            loadingScreen.SetActive(true);

        float startTime = Time.time;

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneNames[sceneType]);
        asyncLoad.allowSceneActivation = false;

        while (!asyncLoad.isDone)
        {
            float elapsedTime = Time.time - startTime;

            if (asyncLoad.progress >= 0.9f && elapsedTime > minimumLoadTime)
            {
                asyncLoad.allowSceneActivation = true;
            }

            yield return null;
        }

        if (loadingScreen != null)
            loadingScreen.SetActive(false);
    }

    public void StartNewGame()
    {
        // Add any initialization logic here if needed
        LoadScene(SceneType.Game);
    }

    public void RestartGame()
    {
        // This method can include any reset logic specific to restarting mid-game
        LoadScene(SceneType.Game);
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    public SceneType GetCurrentSceneType()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        foreach (var kvp in sceneNames)
        {
            if (kvp.Value == currentSceneName)
                return kvp.Key;
        }
        Debug.LogWarning("Current scene not found in SceneType enum");
        return SceneType.MainMenu; // Default to MainMenu if not found
    }
}