using UnityEngine;

public class Enemy : MonoBehaviour
{
    public  void Move(Vector3 direction, float speed )
    {
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }
}