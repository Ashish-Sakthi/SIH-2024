using UnityEngine;

public class MoveWithButtons : MonoBehaviour
{
    [SerializeField] private float MoveSpeed = 1f;
    [SerializeField] private GameObject Player;

    public void MoveForward()
    {
        Player.transform.Translate(Player.transform.forward * MoveSpeed * Time.deltaTime);
    }

    public void MoveBackward()
    {
        Player.transform.Translate(Player.transform.forward * MoveSpeed * Time.deltaTime * -1);
    }
}
