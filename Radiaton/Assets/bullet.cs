using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class shooting : MonoBehaviour
{
    public Transform shootingPoint;
    public GameObject playerBulletPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Instantiate(playerBulletPrefab, shootingPoint.position, transform.rotation);
        }
    }
}
