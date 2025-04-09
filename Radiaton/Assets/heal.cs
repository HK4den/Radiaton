using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heal : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter2D(Collision2D collision)
{
   if(collision.gameObject.name == "Heal Slug" || collision.gameObject.name == "big heal slug")
   {
        Destroy(collision.gameObject);
   }
}

}
