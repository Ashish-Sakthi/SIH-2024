using Oculus.Voice.Dictation;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpeechToText : MonoBehaviour
{
    public AppDictationExperience dictationExperience;

    // Reference to the TextMeshProUGUI where the result will be displayed
    public TextMeshProUGUI resultText;

    // Start dictation
    public void StartDictation()
    {
        if (dictationExperience == null || resultText == null)
        {
            Debug.LogError("DictationExperience or TextMeshProUGUI reference is missing!");
            return;
        }

        // Set up dictation callbacks
        dictationExperience.DictationEvents.onPartialTranscription.AddListener(OnPartialTranscription);
        dictationExperience.DictationEvents.onFullTranscription.AddListener(OnFullTranscription);
        dictationExperience.DictationEvents.onError.AddListener(OnDictationError);

        // Start recording
        dictationExperience.Activate();
        Debug.Log("Starts recording");
    }

    // Stop dictation
    public void StopDictation()
    {
        // Stop dictation
        dictationExperience.Deactivate();
        Debug.Log("Stops recording");

        // Remove callbacks to avoid duplicate listeners
        dictationExperience.DictationEvents.onPartialTranscription.RemoveListener(OnPartialTranscription);
        dictationExperience.DictationEvents.onFullTranscription.RemoveListener(OnFullTranscription);
        dictationExperience.DictationEvents.onError.RemoveListener(OnDictationError);
    }

    private void OnPartialTranscription(string transcription)
    {
        // Update the TextMeshProUGUI with partial results if desired
        resultText.text = transcription;
    }

    private void OnFullTranscription(string transcription)
    {
        // Update the TextMeshProUGUI with the final result
        resultText.text = transcription;
    }

    private void OnDictationError(string error, string message)
    {
        Debug.LogError($"Dictation Error: {error} - {message}");
    }
}
