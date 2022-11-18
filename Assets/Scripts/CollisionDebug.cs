using UnityEngine;

public class CollisionDebug : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D coll)
    {
        Debug.Log("Physics collision!");
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger collision!");
    }
}
