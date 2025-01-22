using UnityEngine;

public class DisappearOnCollision2D : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the colliding object has a Rigidbody
        if (collision.rigidbody != null)
        {
            // Destroy this game object
            Destroy(gameObject);
        }
    }
}
