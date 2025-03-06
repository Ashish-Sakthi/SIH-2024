using UnityEngine;

[CreateAssetMenu(fileName = "ImageSetting", menuName = "ScriptableObjects/ImageSetting", order = 3)]
public class ImageSetting : ScriptableObject
{
    [Header("Image 1 Settings")]
    public Sprite image1;
    public bool useImage1;

    [Header("Image 2 Settings")]
    public Sprite image2;
    public bool useImage2;

    [Header("Image 3 Settings")]
    public Sprite image3;
    public bool useImage3;

    [Header("Image 4 Settings")]
    public Sprite image4;
    public bool useImage4;
}
