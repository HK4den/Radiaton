using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 public class DestroyGameObject: MonoBehaviour
{
 void OnCollisionEnter2D(Collision2D collision)
 {
    destroy(this.GameObject);
 }
}