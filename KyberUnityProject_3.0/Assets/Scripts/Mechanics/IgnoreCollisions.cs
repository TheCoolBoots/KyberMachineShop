
using UnityEngine;

public class IgnoreCollisions : MonoBehaviour
{
    [SerializeField] private Collider collider1;
    [SerializeField] private Collider collider2;

    private void Awake()
    {
        Physics.IgnoreCollision(collider1, collider2, true);
    }
} 
