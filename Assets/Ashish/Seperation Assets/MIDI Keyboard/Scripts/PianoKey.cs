using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class PianoKey : MonoBehaviour /*IPointerEnterHandler, IPointerExitHandler,*/
{
    public int tone, octave;
    public PianoPitcher pitcher;
    GameObject piano;
    AudioClip[] clip;
    AudioMixerGroup group;
    public AudioSource curr;
    public GameObject AudioSourceObject;
    float volume = 0.25f;
    float scale = Mathf.Pow(2f, 1.0f / 12f);
    //bool needtoplay = true;

    void Start()
    {
        AudioSourceObject = FindObjectOfType<OVRCameraRig>().gameObject;
        curr =AudioSourceObject.GetComponent<AudioSource>();
        clip = pitcher.clip;
        group = pitcher.group;
        piano = pitcher.piano;
    }
    //public void OnPointerDown(PointerEventData eventData) //what happens when the key is pressed
    //{
    //    PlayNote();
    //    GetComponent<Animator>().SetBool("down", true);
    //}
    //public void OnPointerUp(PointerEventData eventData)  //what happens when the key gets unpressed
    //{
    //    GetComponent<Animator>().SetBool("down", false);
    //    if (curr != null)
    //    {
    //        StartCoroutine(SoundFade(curr));
    //    }
    //}
    //public void OnPointerEnter(PointerEventData eventData) //some badly working stuff
    //{
    //    if (Input.GetMouseButton(0))
    //    {
    //        GetComponent<Animator>().SetBool("down", true);
    //        PlayNote();
    //    }
    //}
    //public void OnPointerExit(PointerEventData eventData)
    //{

    //    //current = false;
    //    GetComponent<Animator>().SetBool("down", false);
    //    if (curr != null)
    //    {
    //        StartCoroutine(SoundFade(curr));
    //    }
    //}
    private void PlayNote() //this part instantiates new audiosources every time the button is pressed
    {
       
        
        curr = AudioSourceObject.AddComponent<AudioSource>() as AudioSource; 
        curr.loop = true;
        curr.volume = volume;
        curr.outputAudioMixerGroup = group;
        curr.pitch = Mathf.Pow(scale, tone);
        curr.clip = clip[pitcher.octaveOffset + octave - 1];
        curr.Play();
    }
    IEnumerator SoundFade(AudioSource source) //sound fade after the button gets unpressed
    {
        float progress = 0;
        while (progress < 1)
        {
            progress += 0.75f * Time.deltaTime;
            if (source != null)
                source.volume = volume * 1 - progress;
            yield return null;
        }
        Destroy(source);
        yield return null;
    }

    public void PlayButton()
    {
        if(AudioSourceObject.GetComponents<AudioSource>().Length < 3)
        {
            PlayNote();
            GetComponent<Animator>().SetBool("down", true);
        }
        else
        {
            AudioSource[] audios = AudioSourceObject.GetComponents<AudioSource>();
            for (int i = 0; i < audios.Length; i++) 
            {
                Destroy(audios[i]);
            }
            PlayNote();
            GetComponent<Animator>().SetBool("down", true);

        }
       
    }
    public void Unplay()
    {
        GetComponent<Animator>().SetBool("down", false);
        if (curr != null)
        {
            StartCoroutine(SoundFade(curr));
        }
    }
}