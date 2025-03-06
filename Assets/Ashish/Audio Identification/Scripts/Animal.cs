using UnityEngine;

[System.Serializable]
public class Animal : MonoBehaviour
{
    [HideInInspector]public AudioSource audioSource;

    private void Awake()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
                audioSource = gameObject.AddComponent<AudioSource>();
        }
    }
}
