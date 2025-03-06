using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;

using TMPro;
using OVRSimpleJSON;

public class HeartBeat : MonoBehaviour
{
    // Firebase Realtime Database URL
    private string firebaseURL = "https://autism-2b98e-default-rtdb.firebaseio.com/heart_rate.json";

    // Text element to display the fetched data
    public TextMeshProUGUI displayText; // Assign in the Unity Inspector

    void Start()
    {
        // Start fetching data
        StartCoroutine(FetchDataFromFirebase());
    }

    IEnumerator FetchDataFromFirebase()
    {
        while (true) // Infinite loop to continuously fetch data
        {
            // Make a GET request to the Firebase database
            using (UnityWebRequest webRequest = UnityWebRequest.Get(firebaseURL))
            {
                // Wait for the response
                yield return webRequest.SendWebRequest();

                if (webRequest.result == UnityWebRequest.Result.Success)
                {
                    // Parse and display the data
                    Debug.Log($"Data fetched: {webRequest.downloadHandler.text}");
                    UpdateUI(webRequest.downloadHandler.text);
                }
                else
                {
                    // Handle errors
                    Debug.LogError($"Error fetching data: {webRequest.error}");
                }
            }

            // Wait for 5 seconds before the next fetch
            yield return new WaitForSeconds(5f);
        }
    }

    public void OpenURL(string url)
    {
        if (!string.IsNullOrEmpty(url))
        {
            Application.OpenURL(url);
        }
        else
        {
            Debug.LogWarning("URL is empty or null!");
        }
    }

    void UpdateUI(string jsonData)
    {
        if (string.IsNullOrEmpty(jsonData))
        {
            Debug.Log("No data received.");
            return;
        }

        // Parse the JSON data
        var parsedData = JSON.Parse(jsonData);
        long latestTimestamp = 0;
        string latestValue = "";

        foreach (var key in parsedData.Keys)
        {
            if (long.TryParse(key, out long timestamp) && timestamp > latestTimestamp)
            {
                latestTimestamp = timestamp; // Update the latest timestamp
                latestValue = parsedData[key]; // Update the latest value
            }
        }

        // Display only the latest value
        if (displayText != null)
        {
            displayText.text = $"{latestValue}";
        }

        Debug.Log($"Latest Timestamp: {latestTimestamp}, Value: {latestValue}");
    }
    public void UrlSetter(string url)
    {
        firebaseURL = url;
    }
}
