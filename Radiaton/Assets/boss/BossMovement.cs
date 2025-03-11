using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    public float movementSpeed = 10f;
    
     private GameObject player;
    private Rigidbody2D rb;
    private GameObject wall;
      public Vector2 movementDirection;
    
    // Start is called before the first frame update
    void Start()
    {
        wall = GameObject.FindGameObjectWithTag("wall");
     rb = GetComponent<Rigidbody2D>();
    player = GameObject.FindGameObjectWithTag("Player"); // Find the player by tag [1, 2, 7]
     
        
         
    }
    void Update()

    {
       Vector2 movementVector = movementDirection * movementSpeed * Time.deltaTime;

        transform.Translate(movementVector);
    }
       void OnCollisionEnter2D(Collision2D collision) 

{
    
        
    if (gameObject.CompareTag("wall")) 

    {
        movementDirection = (player.transform.position - transform.position).normalized;
    }

}
    
    // Update is called once per frame
   

      
}
