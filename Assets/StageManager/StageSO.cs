using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "StageSO", menuName = "StageSO")]
public class StageSO : ScriptableObject
{
    public enum CompletionType { AfterAudio, CodeTrigger }

    public string stepSubtitle;
    public AudioClip stepAudio;
    public string helpText;
    public AudioClip helpAudio;
    public CompletionType completionType;
    public float helpDelay = 30f; // Delay in seconds for help to appear after step starts

    //public UnityEvent completionEvent;
}

