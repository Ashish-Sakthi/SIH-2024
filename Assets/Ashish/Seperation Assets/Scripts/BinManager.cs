using UnityEngine;

public class BinManager : MonoBehaviour
{
    [SerializeField] private ContainerFruitVeg.ObjectType correctObjectType;
    [SerializeField] private float popHeight = 0.5f;
    [SerializeField] private float moveSpeed = 0.5f;
    [SerializeField] private EndFinderFriut endFinderFriut;

    private void OnTriggerEnter(Collider other)
    {
        ContainerFruitVeg container = other.GetComponent<ContainerFruitVeg>();
        if (container != null)
        {
            if (container.objectType == correctObjectType)
            {
                Debug.Log($"Correct! {container.objectType} dropped in the right bin.");
                --endFinderFriut.total;
            }
            else
            {
                Debug.Log($"Wrong bin! {container.objectType} does not belong here.");
                StartCoroutine(ReturnToInitialPosition(other.gameObject, container.initialPosition));
            }
        }
    }

    private System.Collections.IEnumerator ReturnToInitialPosition(GameObject obj, Vector3 initialPosition)
    {
        Rigidbody rb = obj.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true; // Temporarily disable physics
        }

        Vector3 currentPosition = obj.transform.position;
        Vector3 popUpPosition = currentPosition + Vector3.up * popHeight;

        // Pop up 1 unit
        float elapsedTime = 0f;
        while (elapsedTime < 0.5f)
        {
            obj.transform.position = Vector3.Lerp(currentPosition, popUpPosition, elapsedTime / moveSpeed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Return to the original position
        elapsedTime = 0f;
        while (elapsedTime < 0.5f)
        {
            obj.transform.position = Vector3.Lerp(popUpPosition, initialPosition, elapsedTime / moveSpeed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Re-enable physics
        if (rb != null)
        {
            rb.isKinematic = false;
        }
    }
}
