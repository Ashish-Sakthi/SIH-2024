using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class SensoryProfileControl : MonoBehaviour
{
    public Volume SensoryVolume; // Reference to the post-processing volume
    public Toggle BloomToggle; // Reference to the UI Toggle for Bloom
    public Toggle ColorToggle; // Reference to the UI Toggle for Bloom
    public Toggle AudioToggle; // Reference to the UI Toggle for Bloom

    private Bloom bloom; // Bloom effect from the volume profile
    private ColorAdjustments colorAdjustments;
    public AudioSource CalmaudioSource;
    public AudioSource LoudaudioSource;

    //gameobjects
    //public Toggle Object1Toggle;
    //public Toggle object2Toggle;
    //public Toggle object3Toggle;
    //public Toggle object4Toggle;
    public GameObject[] PersonlisedReward;
    public Button[] PersonalButton;
    //public GameObject Selected;

    public static SensoryProfileControl Instance;
    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    private void OnEnable()
    {
        // Ensure the volume and toggle are assigned
        if (SensoryVolume == null)
        {
            Debug.LogError("SensoryVolume is not assigned.");
            return;
        }

        if (BloomToggle == null)
        {
            Debug.LogError("BloomToggle is not assigned.");
            return;
        }

        // Retrieve the Bloom override from the volume profile
        if (SensoryVolume.profile.TryGet(out bloom))
        {
            // Initialize the toggle based on the current bloom state
            BloomToggle.isOn = bloom.active;

            // Add listener to handle toggle changes
            BloomToggle.onValueChanged.AddListener(ToggleBloom);
            print("ADDED");
        }
        else
        {
            Debug.LogError("Bloom override not found in the Volume Profile.");
        }
        if (SensoryVolume.profile.TryGet(out colorAdjustments))
        {
            // Initialize the toggle based on the current bloom state
            if (!ColorToggle.isOn)
            {
                colorAdjustments.active = true;
            }

            // Add listener to handle toggle changes
            ColorToggle.onValueChanged.AddListener(ToggleColor);
            print("ADDED");
        }
        else
        {
            Debug.LogError("Bloom override not found in the Volume Profile.");
        }

        if (CalmaudioSource && LoudaudioSource)
        {
            AudioToggle.onValueChanged.AddListener(ToggleAudio);
        }

        
    }
    private void Start()
    {
        //// Ensure the volume and toggle are assigned
        //if (SensoryVolume == null)
        //{
        //    Debug.LogError("SensoryVolume is not assigned.");
        //    return;
        //}

        //if (BloomToggle == null)
        //{
        //    Debug.LogError("BloomToggle is not assigned.");
        //    return;
        //}

        //// Retrieve the Bloom override from the volume profile
        //if (SensoryVolume.profile.TryGet(out bloom))
        //{
        //    // Initialize the toggle based on the current bloom state
        //    BloomToggle.isOn = bloom.active;

        //    // Add listener to handle toggle changes
        //    BloomToggle.onValueChanged.AddListener(ToggleBloom);
        //    print("ADDED");
        //}
        //else
        //{
        //    Debug.LogError("Bloom override not found in the Volume Profile.");
        //}
        //if (SensoryVolume.profile.TryGet(out colorAdjustments))
        //{
        //    // Initialize the toggle based on the current bloom state
        //    if(!ColorToggle.isOn)
        //    {
        //        colorAdjustments.active = true;
        //    }

        //    // Add listener to handle toggle changes
        //    ColorToggle.onValueChanged.AddListener(ToggleColor);
        //    print("ADDED");
        //}
        //else
        //{
        //    Debug.LogError("Bloom override not found in the Volume Profile.");
        //}

        //if (CalmaudioSource && LoudaudioSource)
        //{
        //    AudioToggle.onValueChanged.AddListener(ToggleAudio);
        //}
    }

    /// <summary>
    /// Enables or disables the Bloom effect based on the toggle's value.
    /// </summary>
    /// <param name="isOn">The current state of the toggle.</param>
    private void ToggleBloom(bool isOn)
    {
        if (bloom != null)
        {
            bloom.active = isOn;
        }
    }private void ToggleColor(bool isOn)
    {
        if (colorAdjustments != null)
        {
            colorAdjustments.active = !isOn;
        }
    }
    /*public void SelectSprite(int index)
    {
        Selected = PersonlisedReward[index];
        Debug.Log($"Selected sprite: {Selected.name}");
    }*/
    private void ToggleAudio(bool isOn)
    {
        if (CalmaudioSource)
        {
            if(!isOn)
            {
                CalmaudioSource.gameObject.SetActive(true);
                LoudaudioSource.gameObject.SetActive(false) ;
                CalmaudioSource.Play();
                CalmaudioSource.loop = true;
            }
            else
            {
                CalmaudioSource.gameObject.SetActive(false);
               LoudaudioSource.gameObject.SetActive(true);
               LoudaudioSource.Play();
               LoudaudioSource.loop = true;
            }
        }
    }
    

    private void OnDestroy()
    {
        // Remove the listener when the object is destroyed to avoid memory leaks
        if (BloomToggle != null)
        {
            BloomToggle.onValueChanged.RemoveListener(ToggleBloom);
            ColorToggle.onValueChanged.RemoveListener(ToggleColor);
        }
    }
}
