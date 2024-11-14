using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 4;
    public int currentHealth;

    //this is where you'd add the animation
    public Animator anim; 

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }
    void TakeDamage(int amount)
    {
        // Subtracts from your health
        currentHealth -= amount;

        // Makes sure health doesn't go negative and you die. If health goes to 0, you die
        if (currentHealth <= 0)
        {
            // You're dead
            // Play death animation (learn that later!)
            anim.SetBool("IsDead", true);
            // Show Gameover screen (make one and add a main menu and restart button)

        }

    }
}
