using UnityEngine;
using UnityEngine.UI;

public class ToggleManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Toggle toggle1;
    [SerializeField] private Toggle toggle2;
    [SerializeField] private Toggle toggle3;
    [SerializeField] private Toggle toggle4;
    [SerializeField] private Button saveButton;

    [Header("Scriptable Object Reference")]
    [SerializeField] private ImageSetting imageSettings;

    private void Start()
    {
        // Add listeners to toggles
        toggle1.onValueChanged.AddListener((isOn) => OnToggleChanged(isOn, toggle1));
        toggle2.onValueChanged.AddListener((isOn) => OnToggleChanged(isOn, toggle2));
        toggle3.onValueChanged.AddListener((isOn) => OnToggleChanged(isOn, toggle3));
        toggle4.onValueChanged.AddListener((isOn) => OnToggleChanged(isOn, toggle4));

        // Add listener to the button
        saveButton.onClick.AddListener(SaveToScriptableObject);
    }

    private void OnToggleChanged(bool isOn, Toggle changedToggle)
    {
        if (isOn)
        {
            // Turn off all other toggles
            if (changedToggle != toggle1) toggle1.isOn = false;
            if (changedToggle != toggle2) toggle2.isOn = false;
            if (changedToggle != toggle3) toggle3.isOn = false;
            if (changedToggle != toggle4) toggle4.isOn = false;
        }
    }

    private void SaveToScriptableObject()
    {
        if (imageSettings == null)
        {
            Debug.LogError("ImageSettings ScriptableObject is not assigned.");
            return;
        }

        // Update the ScriptableObject based on toggle states
        imageSettings.useImage1 = toggle1.isOn;
        imageSettings.useImage2 = toggle2.isOn;
        imageSettings.useImage3 = toggle3.isOn;
        imageSettings.useImage4 = toggle4.isOn;

        Debug.Log("ScriptableObject updated with toggle values.");
    }
}
