using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spin : MonoBehaviour
{
    public float spinSpeed = 100f; // Rotation speed

    void update()
    {
         transform.Rotate(Vector3.up, spinSpeed * Time.deltaTime);
    }
}