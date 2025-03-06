using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StageManager : MonoBehaviour
{
    public TextMeshProUGUI subtitle;
    public AudioSource AudioHandler;

    public StageSO[] StageSOs; // Assign in Inspector
    public Stages[] Stages; // Gameobjects for each step
    private int currentStepIndex = 0;
    private bool isStepCompleted = true; // Flag to prevent next step from triggering prematurely
    public GameObject _CorrectIcon;
    public AudioClip _CorrectAudio;

    private void Start()
    {
        if (StageSOs != null && StageSOs.Length > 0)
        {
            StartNextStep();
        }
        else
        {
            Debug.LogError("No Stage Steps assigned in the Inspector.");
        }

        //_CorrectIcon = SensoryProfileControl.Instance.Selected;
    }

    public void StartNextStep()
    {
        if (StageSOs != null && currentStepIndex < StageSOs.Length && Stages != null && isStepCompleted)
        {
            isStepCompleted = false;
            StageSO currentStep = StageSOs[currentStepIndex];
            Stages currentStage = Stages[currentStepIndex];

            if (currentStep != null)
            {
                // Setup Subtitle
                if (subtitle != null) subtitle.text = currentStep.stepSubtitle;

                // Step start event
                if (currentStage?.StartEvent != null) currentStage.StartEvent.Invoke();
                Invoke("DelayEventFunc", currentStage?.DelayEventTime ?? 0);

                // Play Step Audio
                StartCoroutine(PlayAudioWithCompletion(AudioHandler, currentStep.stepAudio, () =>
                {
                    if (currentStep.completionType == StageSO.CompletionType.AfterAudio)
                    {
                        StepCompleted();
                    }
                }));

                // Setup and Delay Help Text and Audio
                Invoke(nameof(ShowHelp), currentStep.helpDelay);
            }
        }
        else if (currentStepIndex >= (StageSOs?.Length ?? 0))
        {
            Debug.Log("All steps have been completed.");
        }
    }

    private void ShowHelp()
    {
        if (StageSOs != null && currentStepIndex < StageSOs.Length)
        {
            StageSO currentStep = StageSOs[currentStepIndex];
            if (currentStep != null)
            {
                if (subtitle != null) subtitle.text = currentStep.helpText;
                PlayAudio(AudioHandler, currentStep.helpAudio);
            }
        }
    }

    private void PlayAudio(AudioSource audioSource, AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }

    private IEnumerator PlayAudioWithCompletion(AudioSource audioSource, AudioClip clip, System.Action onCompletion)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.clip = clip;
            audioSource.Play();

            // Wait until the audio has finished playing
            yield return new WaitForSeconds(clip.length);

            // Invoke the completion callback
            onCompletion?.Invoke();
        }
    }

    private void DelayEventFunc()
    {
        if (Stages != null && currentStepIndex < Stages.Length)
        {
            Stages currentStage = Stages[currentStepIndex];
            if (currentStage?.DelayEvent != null) currentStage.DelayEvent.Invoke();
        }
    }

    public void StepCompleted() // Call this from your code trigger if CompletionType is CodeTrigger
    {
        if (Stages != null && currentStepIndex < Stages.Length)
        {
            isStepCompleted = true;
            Stages[currentStepIndex]?.EndEvent?.Invoke();
            //CorrectIcon();
            currentStepIndex++;

            if (currentStepIndex < Stages.Length)
            {
                Stages currentStage = Stages[currentStepIndex];
                Invoke(nameof(StartNextStep), 2 + (currentStage?.StepCompletionTime ?? 0));
            }
        }
    }

    public void StepJump(int index) // Call this from your code trigger if CompletionType is CodeTrigger
    {
        if (Stages != null && currentStepIndex < Stages.Length)
        {
            isStepCompleted = true;
            Stages[currentStepIndex]?.EndEvent?.Invoke();
            //CorrectIcon();
            currentStepIndex = index;

            if (currentStepIndex < Stages.Length)
            {
                Stages currentStage = Stages[currentStepIndex];
                Invoke(nameof(StartNextStep), 2 + (currentStage?.StepCompletionTime ?? 0));
            }
            Debug.Log("Next step");
        }
    }
        public void CorrectIcon()
    {
        StartCoroutine("CorrectIconFunc");
    }

    public IEnumerator CorrectIconFunc()
    {
        if (_CorrectIcon != null)
        {
            _CorrectIcon.SetActive(true);
        }
        if (AudioHandler != null && _CorrectAudio != null)
        {
            AudioHandler.PlayOneShot(_CorrectAudio);
        }
        yield return new WaitForSeconds(2.5f);
        if (_CorrectIcon != null)
        {
            _CorrectIcon.SetActive(false);
        }
    }
}
