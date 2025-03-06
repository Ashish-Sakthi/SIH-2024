using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathsCalculator : MonoBehaviour
{
    [SerializeField] private StageManager stageManager;
    [SerializeField] private Animator animator;
    [SerializeField] private int need = 1;
    [SerializeField] private int have = 10;
    [SerializeField] private int given = 0;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Chocolate"){
            Debug.Log("Hi");
            if (given < need)
            {
                given++;
                have--;
                if (given == need)
                {
                    //Completed
                    animator.SetTrigger("Take");
                    stageManager.CorrectIcon();
                    stageManager.StepCompleted();
                }
            }
        }
    }

    public void ResetValues(int need)
    {
        given = 0;
        this.need = need;

    }
    public void SetHave(int have)
    {
        this.have = have;
    }
}
