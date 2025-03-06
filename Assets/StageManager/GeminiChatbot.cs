using Meta.WitAi.TTS.Interfaces;
using Meta.WitAi.TTS.Utilities;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class GeminiChatbot : MonoBehaviour
{
    public TextMeshProUGUI question;
    public string customprompt;
    public TextMeshProUGUI answer;

    public TTSSpeaker speaker;

    [SerializeField] public string prompt;
    private string gasURL = "https://script.google.com/macros/s/AKfycbx0d3-FP7Zaq97_NuokcdmjPs1THH8r7lPtWQTwvqcWmf20oUBVKvWfLZd7BASoMMIt/exec";

    public void SpeakGemini()
    {
        StartCoroutine(SendDataToGAS());
    }

    public void SpeakStep(string pr)
    {
        StartCoroutine(StepPrompt(pr));
    }

    public void SpeakSpidy(string pr)
    {
        StartCoroutine(Spiderman(pr));
    }

    private IEnumerator SendDataToGAS()
    {
        WWWForm form = new WWWForm();
        form.AddField("parameter", customprompt + question.text );
        UnityWebRequest www = UnityWebRequest.Post(gasURL, form);

        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            answer.text = www.downloadHandler.text;
        }
        else
        {
            answer.text = $"Error: {www.error}";
        }

        speaker.SpeakQueued(answer.text);
        Debug.Log("Response: " + answer.text);
    }

    private IEnumerator StepPrompt(string stprompt)
    {
        WWWForm form = new WWWForm();
        form.AddField("parameter", "You are taken a role of autism child mother" + question.text + "this is the previous reply of child -- " + stprompt + " but regardless of saying yes or no.. he is not eating" + "give a reply to him based on his previous answer with single dialogue, dont give any single word in response");
        UnityWebRequest www = UnityWebRequest.Post(gasURL, form);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            answer.text = www.downloadHandler.text;
        }
        else
        {
            answer.text = $"Error: {www.error}";
        }

        speaker.SpeakQueued(answer.text);
        Debug.Log("Response: " + answer.text);
    }

    private IEnumerator Spiderman(string stprompt)
    {
        WWWForm form = new WWWForm();
        form.AddField("parameter", "You are spiderman, taken a role of speaking with autism child." + question.text + "--- this is the previous reply of child." + stprompt + " .regardless of his reply we need to train how to greet a friend" + "give a reply to him in a single line, response length should be very small");
        UnityWebRequest www = UnityWebRequest.Post(gasURL, form);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            answer.text = www.downloadHandler.text;
        }
        else
        {
            answer.text = $"Error: {www.error}";
        }

        speaker.SpeakQueued(answer.text);
        Debug.Log("Response: " + answer.text);
    }
}
