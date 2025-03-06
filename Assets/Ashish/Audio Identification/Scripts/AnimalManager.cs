using UnityEngine;

public class AnimalManager : MonoBehaviour
{
    [System.Serializable]
    public class Animal
    {
        public GameObject prefab;     // The prefab of the animal
        public AudioClip sound;
        public Transform spawnPoint;
    }

    public Animal[] animals;          // Array to hold animal details
         // The point where animals will be spawned
    private GameObject currentAnimal; // Reference to the currently active animal
    private AudioSource audioSource;  // Audio source for playing animal sounds

    void Start()
    {
        // Ensure there's an AudioSource component on this GameObject
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    public void SpawnAnimal(int index)
    {
        // Destroy the currently active animal and stop its sound
        if (currentAnimal != null)
        {
            Destroy(currentAnimal);
            audioSource.Stop();
        }

        // Instantiate the new animal
        currentAnimal = Instantiate(animals[index].prefab, animals[index].spawnPoint.position, animals[index].spawnPoint.rotation);

        // Play the associated sound
        audioSource.clip = animals[index].sound;
        audioSource.Play();

        // Start the animation if available
        Animator animator = currentAnimal.GetComponent<Animator>();
        if (animator != null)
        {
            animator.Play("DefaultAnimation"); // Replace "DefaultAnimation" with your animation state name
        }
    }
}
