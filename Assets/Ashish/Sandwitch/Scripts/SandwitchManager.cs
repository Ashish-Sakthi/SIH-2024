using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandwitchManager : MonoBehaviour
{
    public void TurnOffAllGrabbable()
    {
        TouchHandGrabInteractable[] interactables = FindObjectsOfType<TouchHandGrabInteractable>();
        foreach (var interactable in interactables)
        {
            interactable.enabled = false;
        }
    }
}
