using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossshooting : MonoBehaviour
{
    public GameObject bullet;
   

    private float timer;
    // Start is called before the first frame update
    

public Transform[] bulletPositions; // Array of desired spawn locations



    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
      timer += Time.deltaTime;
        if(timer > .50)
        {
            timer = 0;
            shoot();
        }
      
    }
    void shoot()
    { foreach (Transform position in bulletPositions) {

        Instantiate(bullet, transform.position, Quaternion.identity); 

    }
      
    }
}
