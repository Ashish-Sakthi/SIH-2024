using UnityEngine;

public class Stick : MonoBehaviour
{
    private Rigidbody rb;
    public OVRInput.Controller controllerType = OVRInput.Controller.RTouch; // Set to the appropriate controller (RTouch or LTouch)

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Instrument>())
        {
            
          
        }
    }

   
}
