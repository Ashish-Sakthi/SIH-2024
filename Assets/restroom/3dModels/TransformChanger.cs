using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformChanger : MonoBehaviour
{
   public void TransformChange(Transform t)
    {
        this.gameObject.transform.position = t.position;
        this.gameObject.transform.rotation = t.rotation;
    }
}
