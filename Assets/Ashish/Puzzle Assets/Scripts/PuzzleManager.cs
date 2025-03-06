using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    [SerializeField]private List<GameObject> Levels;
    [SerializeField]private StageManager stageManager;
    [SerializeField]private SnapPoint[] snapPoint;
    //[SerializeField]private Canvas canvas;
    private int lvlval = 0;
    public int remainingPieces;

    public void FindRemainingPeices()
    {
        snapPoint = FindObjectsOfType<SnapPoint>();
        remainingPieces = snapPoint.Length;
        print("In Pm - rem = " + remainingPieces);
    }

    public void CheckRemainingPieces()
    {
        if (remainingPieces == 0)
        {
            stageManager.StepCompleted();
            //canvas.enabled = true;
            
        }
    }

    //public void NextLvl()
    //{
    //    if(lvlval >= Levels.Count)
    //        return;
    //    Levels[lvlval].SetActive(false);

    //    Levels[++lvlval].SetActive(true);
    //}

    
}
