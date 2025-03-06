using UnityEngine;

public class PersistantUi : MonoBehaviour
{
    private static PersistantUi instance;

    private void Awake()
    {
        // Ensure this GameObject is a root object
        if (transform.parent != null)
        {
            Debug.LogError("PersistentUIManager must be attached to a root GameObject.");
            return;
        }

        // Check if an instance already exists
        if (instance != null && instance != this)
        {
            Destroy(gameObject); // Destroy duplicate
            return;
        }

        // Set this as the instance and make it persistent
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

}
