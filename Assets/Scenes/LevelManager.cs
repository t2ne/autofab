using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    public Slider progressBar;
    public GameObject transitionsContainer;

    private SceneTransition[] transitions;
    private bool hasInitialized = false;
    private bool isLoadingScene = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // Initialize transitions here instead of in Start
            InitializeTransitions();

            // Register scene loaded event to handle any scene-specific setup
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeTransitions()
    {
        if (!hasInitialized && transitionsContainer != null)
        {
            transitions = transitionsContainer.GetComponentsInChildren<SceneTransition>();
            hasInitialized = true;
            Debug.Log("LevelManager: Transitions initialized");
        }
    }

    // Called when a scene is loaded
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log($"Scene loaded: {scene.name} with mode: {mode}");
        isLoadingScene = false;
    }

    public void LoadScene(string sceneName, string transitionName)
    {
        // Prevent multiple scene loads at the same time
        if (isLoadingScene)
        {
            Debug.Log("Already loading a scene, request ignored");
            return;
        }

        if (SceneManager.GetActiveScene().name == sceneName)
        {
            Debug.Log($"Already in scene {sceneName}, load request ignored");
            return;
        }

        StartCoroutine(LoadSceneAsync(sceneName, transitionName));
    }

    private IEnumerator LoadSceneAsync(string sceneName, string transitionName)
    {
        isLoadingScene = true;

        // Make sure transitions are initialized
        if (!hasInitialized)
        {
            InitializeTransitions();
        }

        // Find the requested transition
        SceneTransition transition = transitions.FirstOrDefault(t => t.name == transitionName);

        if (transition == null)
        {
            Debug.LogError($"Transition '{transitionName}' not found!");
            isLoadingScene = false;
            yield break;
        }

        AsyncOperation scene = SceneManager.LoadSceneAsync(sceneName);
        scene.allowSceneActivation = false;

        yield return transition.AnimateTransitionIn();

        if (progressBar != null)
        {
            progressBar.gameObject.SetActive(true);

            do
            {
                progressBar.value = scene.progress;
                yield return null;
            } while (scene.progress < 0.9f);

            yield return new WaitForSeconds(1f);
            progressBar.gameObject.SetActive(false);
        }

        scene.allowSceneActivation = true;

        // Wait for the scene to be fully loaded before transitioning out
        while (!scene.isDone)
        {
            yield return null;
        }

        yield return transition.AnimateTransitionOut();
    }

    // Clean up when the object is destroyed
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
