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
       // if (collision.tag == "Player")
        //{
            //checks for a health component, if found then it damages
            //var healthComponent = collision.GetComponent<healthComponent>();
            //if (healthComponent != null)
            //{
               // healthComponent.TakeDamage(1);
           // }
        //}
    }
}