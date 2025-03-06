using UnityEngine;

public class MatchableItem : MonoBehaviour
{
    private ItemManager itemManager;

    private void Start()
    {
        itemManager = FindObjectOfType<ItemManager>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject otherItem = collision.gameObject;
        if (itemManager != null && otherItem.GetComponent<MatchableItem>() != null)
        {
            if (itemManager.IsMatch(gameObject, otherItem))
            {
                itemManager.CompleteMatch(gameObject, otherItem);
            }
        }
    }
}
