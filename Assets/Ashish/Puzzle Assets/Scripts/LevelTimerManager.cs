using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelTimerManager : MonoBehaviour
{
    public GameObject timerPrefab; // Prefab with TextMeshPro for each timer
    public Transform timersPanel; // Parent panel for all timers
    private List<GameObject> timerObjects = new List<GameObject>(); // Store the timer GameObjects
    private List<float> levelTimes = new List<float>();
    private int currentLevel = -1; // Current level index (start at -1 to ensure no timer is active initially)
    private bool isLevelActive = false;

    void Start()
    {
        // Initial setup if necessary
    }

    void Update()
    {
        if (isLevelActive && currentLevel >= 0 && currentLevel < levelTimes.Count)
        {
            // Update the current level's timer
            levelTimes[currentLevel] += Time.deltaTime;
            UpdateTimerText(currentLevel);
        }
    }

    // Add a new timer for the current level
    public void AddTimer()
    {
        // Instantiate a new timer for the current level
        GameObject newTimer = Instantiate(timerPrefab, timersPanel);
        TextMeshProUGUI textComponent = newTimer.GetComponent<TextMeshProUGUI>();

        // Add the "Level N" label before the timer
        textComponent.text = "Level " + (currentLevel + 1); // Level numbers start from 1

        timerObjects.Add(newTimer); // Add the new timer GameObject to the list
        levelTimes.Add(0f); // Initialize the timer at 0

        UpdateTimerText(currentLevel); // Update the display for the new timer
    }

    // Start the new level and replace the previous timer with the new one
    public void StartLevel(int levelIndex)
    {
        // If there's an existing level timer, pause it first
        if (currentLevel >= 0 && currentLevel < levelTimes.Count)
        {
            DisablePreviousTimer(currentLevel); // Disable the previous level timer
        }

        // Update the current level to the new level
        currentLevel = levelIndex;

        // Add the timer for the new level
        AddTimer();

        // Start the new level's timer
        isLevelActive = true;
    }

    // Complete the current level and stop the timer but keep it visible
    public void CompleteLevel()
    {
        isLevelActive = false; // Stop the timer but don't destroy the timer
    }

    // Disable the previous level timer by deactivating the entire GameObject
    private void DisablePreviousTimer(int levelIndex)
    {
        if (levelIndex >= 0 && levelIndex < timerObjects.Count)
        {
            // Deactivate the entire GameObject to stop the timer
            timerObjects[levelIndex].SetActive(false);
        }
    }

    // Update the timer text for a specific level
    private void UpdateTimerText(int levelIndex)
    {
        int minutes = Mathf.FloorToInt(levelTimes[levelIndex] / 60f);
        int seconds = Mathf.FloorToInt(levelTimes[levelIndex] % 60f);
        timerObjects[levelIndex].GetComponent<TextMeshProUGUI>().text = $"Level {levelIndex + 1} {minutes:00}:{seconds:00}";
    }

    // Public method to calculate the total time for all levels
    public void GetTotalTime()
    {
        float totalTime = 0f;
        foreach (float time in levelTimes)
        {
            totalTime += time;
        }

        int totalMinutes = Mathf.FloorToInt(totalTime / 60f);
        int totalSeconds = Mathf.FloorToInt(totalTime % 60f);
        Debug.Log($"{totalMinutes:00}:{totalSeconds:00}");
    }
}
