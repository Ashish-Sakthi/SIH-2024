using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance { get; private set; }

    [SerializeField] private Slider  progressBar; // Reference to the slider component
    //[SerializeField] private TextMeshProUGUI progressText; // (Optional) Text to show progress percentage
    public GameObject Load;

    public GameObject[] panels;

    private string sceneToLoad;
    private int sceneIndexToLoad;

    private void Awake()
    {
        // Singleton setup
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
       
    }

    public void SetNextPage(int nextPageIndex)
    {
        // Disable all UI elements
        foreach (GameObject uiElement in panels)
        {
            uiElement.SetActive(false);
        }

        // Enable the specified UI element if within bounds
        if (nextPageIndex >= 0 && nextPageIndex < panels.Length)
        {
            panels[nextPageIndex].SetActive(true);
        }
        else
        {
            Debug.LogWarning("Invalid index passed to SetNextPage!");
        }
    }


    /// <summary>
    /// Load a scene by name.
    /// </summary>
    /// <param name="sceneName">The name of the scene to load.</param>
    public void LoadSceneByName(string sceneName)
    {
        Load.SetActive(true);
        sceneToLoad = sceneName;
        StartCoroutine(LoadSceneCoroutine());
    }

    /// <summary>
    /// Load a scene by index.
    /// </summary>
    /// <param name="sceneIndex">The build index of the scene to load.</param>
    public void LoadSceneByIndex(int sceneIndex)
    {
        sceneIndexToLoad = sceneIndex;
        StartCoroutine(LoadSceneByIndexCoroutine());
    }

    /// <summary>
    /// Coroutine to load a scene by name asynchronously.
    /// </summary>
    private IEnumerator LoadSceneCoroutine()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneToLoad);
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            UpdateProgress(operation.progress);

            if (operation.progress >= 0.9f)
            {
                operation.allowSceneActivation = true;
            }

            yield return null;
        }
        Load.SetActive(false);
    }

    /// <summary>
    /// Coroutine to load a scene by index asynchronously.
    /// </summary>
    private IEnumerator LoadSceneByIndexCoroutine()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndexToLoad);
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            UpdateProgress(operation.progress);

            if (operation.progress >= 0.9f )
            {
                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }

    /// <summary>
    /// Updates the progress bar and text based on loading progress.
    /// </summary>
    /// <param name="progress">The progress value (0 to 1).</param>
    private void UpdateProgress(float progress)
    {
        float normalizedProgress = Mathf.Clamp01(progress / 0.9f);
        progressBar.value = normalizedProgress;

        //if (progressText != null)
        //{
        //    progressText.text = $"{(normalizedProgress * 100):0}%";
        //}
    }
}
