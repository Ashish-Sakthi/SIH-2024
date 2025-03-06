using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [System.Serializable]
    public class MatchPair
    {
        public GameObject Item1;
        public GameObject Item2;
    }

    public List<MatchPair> Matches = new List<MatchPair>();
    public Color MatchColor = Color.green;

    private Dictionary<GameObject, GameObject> matchDictionary = new Dictionary<GameObject, GameObject>();

    private void Start()
    {
        foreach (var pair in Matches)
        {
            if (pair.Item1 != null && pair.Item2 != null)
            {
                matchDictionary[pair.Item1] = pair.Item2;
                matchDictionary[pair.Item2] = pair.Item1;
            }
        }
    }

    public bool IsMatch(GameObject itemA, GameObject itemB)
    {
        return matchDictionary.ContainsKey(itemA) && matchDictionary[itemA] == itemB;
    }

    public void CompleteMatch(GameObject itemA, GameObject itemB)
    {
        Debug.Log($"Matched: {itemA.name} with {itemB.name}");

        SetColor(itemA, MatchColor);
        SetColor(itemB, MatchColor);

        //itemA.GetComponent<Collider>().enabled = false;
        //itemB.GetComponent<Collider>().enabled = false;
    }

    private void SetColor(GameObject obj, Color color)
    {
        if (obj.TryGetComponent(out Renderer renderer))
        {
            renderer.material.color = color;
        }
    }
}
