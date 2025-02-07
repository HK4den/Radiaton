using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Spin : MonoBehaviour

{

    public float rotationSpeed = 30f; // Adjust this value to control rotation speed



    void Update()

    {

        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime); // Rotate on the Z-axis

    }

}
