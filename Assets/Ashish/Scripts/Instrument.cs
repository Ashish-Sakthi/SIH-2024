using UnityEngine;

public class Instrument : MonoBehaviour
{
    public AudioClip baseClip; // Base sound for the instrument
    private AudioSource audioSource;
    private Renderer instrumentRenderer;
    public Color hitColor = Color.red; // Color to flash when hit
    private Color originalColor;
    private Transform OriginalTransform;

    void Start()

    {
        // Configure AudioSource
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = baseClip;
        audioSource.playOnAwake = false;

        // Save original material color for visual feedback
        instrumentRenderer = GetComponent<Renderer>();
        originalColor = instrumentRenderer.material.color;
       
        OriginalTransform = transform;
    }


    void OnCollisionEnter(Collision collision)
    {
        // Ensure the interacting object is the stick
        if (collision.gameObject.CompareTag("Stick"))
        {
            // Calculate the interaction intensity
            Rigidbody stickRigidbody = collision.rigidbody;
            float interactionSpeed = stickRigidbody != null ? stickRigidbody.velocity.magnitude : 1.0f;

            if (!audioSource.isPlaying) { audioSource.Play(); }

           
            // Play sound dynamically based on speed
           // PlayDynamicSound(interactionSpeed);

            // Provide visual feedback
            StartCoroutine(VisualFeedback());
        }
        
       
    }

    private void PlayDynamicSound(float intensity)
    {
        audioSource.volume = Mathf.Clamp(intensity / 10.0f, 0.1f, 1.0f); // Map speed to volume
        audioSource.pitch = Mathf.Clamp(1.0f + (intensity / 10.0f), 0.8f, 1.5f); // Map speed to pitch
       
    }

    
    private System.Collections.IEnumerator VisualFeedback()
    {
        instrumentRenderer.material.color = hitColor; // Change to hit color
        yield return new WaitForSeconds(0.1f); // Flash duration
        instrumentRenderer.material.color = originalColor; // Revert to original color
    }
}
