using UnityEngine;
using UnityEngine.UI;

public class ImageHandler : MonoBehaviour
{
    [SerializeField] public ImageSetting imageSettings;

    public Image image;

    public void OnEnable()
    {
        if (imageSettings.useImage1)
        {
            Debug.Log("Using Image 1");
            image.sprite = imageSettings.image1;
            // Do something with imageSettings.image1
        }

        if (imageSettings.useImage2)
        {
            Debug.Log("Using Image 2");
            image.sprite = imageSettings.image2;
            // Do something with imageSettings.image2
        }

        if (imageSettings.useImage3)
        {
            Debug.Log("Using Image 2");
            image.sprite = imageSettings.image2;
            // Do something with imageSettings.image2
        }

        if (imageSettings.useImage4)
        {
            Debug.Log("Using Image 2");
            image.sprite = imageSettings.image2;
            // Do something with imageSettings.image2
        }
    }
}
