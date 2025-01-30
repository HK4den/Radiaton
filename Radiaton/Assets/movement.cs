using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    public float speed;
private Rigidbody2D rb;

void start()
{
   rb = GetComponent<Rigidbody2D>();
   rb.velocity = transform.right * speed;
}



}