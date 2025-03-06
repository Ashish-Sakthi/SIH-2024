using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindCorrectIng : MonoBehaviour
{
    [SerializeField] private StageManager stageManager; 
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == gameObject.name)
        {
            other.gameObject.transform.position = gameObject.transform.position;

            stageManager.StepCompleted();
        }
    }
    
}
