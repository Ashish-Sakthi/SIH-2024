using UnityEngine;

public class ContainerFruitVeg : MonoBehaviour
{
    public enum ObjectType { Fruit, Vegetable }
    public ObjectType objectType;              

    [HideInInspector] public Vector3 initialPosition;

    private void Start()
    {
        initialPosition = transform.position;
    }
}
