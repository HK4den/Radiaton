using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
   
    void Start()
    {
        
    }

    //collision with enemy and bullet stuff
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Bullet"))
        {
            PlayerTakeDmg(1);
            Debug.Log("Player hit by: " + collision.gameObject.tag);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Enemy") || collider.CompareTag("Bullet"))
        {
            PlayerTakeDmg(1);
            Debug.Log("Player hit by: " + collider.tag);
        }
    }

    void Update()
    {
        //testing player health system
        if (Input.GetKeyDown(KeyCode.E))
        {
            PlayerTakeDmg(1);
            Debug.Log(GameManager.gameManager._playerHealth.Health);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            PlayerHeal(1);
            Debug.Log(GameManager.gameManager._playerHealth.Health);
        }
        


    }

    private void PlayerTakeDmg(int dmg)
    {
        GameManager.gameManager._playerHealth.DmgUnit(dmg);
        Debug.Log(GameManager.gameManager._playerHealth.Health);
    }

    private void PlayerHeal(int healing)
    {
        GameManager.gameManager._playerHealth.HealUnit(healing);
    }
}
