using UnityEngine;

public class DisappearOnCollision : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the colliding object has a Rigidbody2D
        if (collision.rigidbody != null)
        {
            // Destroy this game object
            Destroy(gameObject);
        }
    }
}
