
using UnityEngine;

public class IgnoreCollisions : MonoBehaviour
{
    [SerializeField] private Collider rootCollider;
    [SerializeField] private System.Collections.Generic.List<Collider> collidersToIgnore;

    private void Awake()
    {
        foreach(Collider c in collidersToIgnore)
        {
            Physics.IgnoreCollision(c, rootCollider, true);
        }
        
    }
} 
