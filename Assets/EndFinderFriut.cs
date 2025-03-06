using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndFinderFriut : MonoBehaviour
{
    [SerializeField] private StageManager stageManager; 
    public int total = 9;

   
    void Update()
    {
        if(total == 0)
        {
            stageManager.StepCompleted();
        }
    }
}
