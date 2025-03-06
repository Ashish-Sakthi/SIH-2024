using Oculus.Interaction;
using System.Threading.Tasks;
using UnityEngine;
//using UnityEngine.XR.Interaction.Toolkit;

public class PuzzlePiece : MonoBehaviour
{
    [SerializeField] private SnapPoint[] SnapList;
    [SerializeField] private PuzzleManager puzzleManager;
    [SerializeField] float snapRange = 0.05f; 
    [SerializeField] float snapSpeed = 1f; 

    private SnapPoint nearestSnapPoint;
    private float nearestSnapDistance;
    public bool isSnapped = false;

    //private XRGrabInteractable grabInteractable;
    [SerializeField] private TouchHandGrabInteractable THgrabbable;
    private Vector3 initialPosition;
    private Quaternion initialRotation;

    private void Start()
    {
        SnapList = FindObjectsOfType<SnapPoint>();
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }


    private void Update()
    {
        if (!isSnapped)
        {
            FindNearestSnap();
            if (nearestSnapDistance <= snapRange && nearestSnapPoint.correctPiece == gameObject)
            {
                MoveToTargetAsync(nearestSnapPoint.transform).Forget();
                isSnapped = true;
                LockPiece();

            }
        }
    }

    private void FindNearestSnap()
    {
        nearestSnapDistance = Mathf.Infinity;

        foreach (var snapPoint in SnapList)
        {
            float distance = Vector3.Distance(snapPoint.transform.position, transform.position);
            if (distance < nearestSnapDistance)
            {
                nearestSnapDistance = distance;
                nearestSnapPoint = snapPoint;
            }
        }
    }

    private async Task MoveToTargetAsync(Transform targetTransform)
    {
        float elapsedTime = 0f;
        Vector3 startPosition = transform.position;
        Quaternion startRotation = transform.rotation;

        while (elapsedTime < snapSpeed)
        {
            transform.position = Vector3.Lerp(startPosition, targetTransform.position, elapsedTime / snapSpeed);
            transform.rotation = Quaternion.Lerp(startRotation, targetTransform.rotation, elapsedTime / snapSpeed);
            elapsedTime += Time.deltaTime;

            await Task.Yield();
        }

        transform.position = targetTransform.position;
        transform.rotation = targetTransform.rotation;
        puzzleManager.remainingPieces--;
        puzzleManager.CheckRemainingPieces();
    }

    private void LockPiece()
    {
        //if (grabInteractable != null)
        //{
        //    grabInteractable.enabled = false;
        //}
        if (THgrabbable != null)
        {
            THgrabbable.enabled = false;
        }
        //puzzleManager.remainingPieces--;
        //puzzleManager.CheckRemainingPieces();
        // Lock Rigidbody if present
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
        }
    }

    public async Task ResetToInitialPositionAsync()
    {
        float resetDuration = 0.2f;
        float elapsedTime = 0f;
        Vector3 currentPos = transform.position;

        while (elapsedTime < resetDuration)
        {
            transform.position = Vector3.Lerp(currentPos, initialPosition, elapsedTime / resetDuration);
            transform.rotation = Quaternion.Lerp(transform.rotation, initialRotation, elapsedTime / resetDuration);
            elapsedTime += Time.deltaTime;

            await Task.Yield();
        }

        transform.position = initialPosition;
        transform.rotation = initialRotation;
    }
}

// Extension method for fire-and-forget tasks
public static class TaskExtensions
{
    public static void Forget(this Task task)
    {
        task.ContinueWith(t =>
        {
            if (t.IsFaulted)
            {
                Debug.LogError(t.Exception);
            }
        });
    }
}
