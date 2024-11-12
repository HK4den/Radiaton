using UnityEngine;

public class DisappearOnCollision : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        // Check if the colliding object has a Rigidbody
        if (collision.rigidbody != null)
        {
            // Destroy this game object
            Destroy(gameObject);
        }
    }
}
