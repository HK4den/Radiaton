using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shotgunShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
   

    private float timer;
    // Start is called before the first frame update
    


public Transform firePointLeft;

public Transform firePointRight;
public Transform firePointLeftUp;
public Transform firePointRightUp;
public Transform firePointRightDown;






    // Update is called once per frame
    void Update()
    {
      timer += Time.deltaTime;
        if(timer > 2)
        {
            timer = 0;
            Shoot();
        }
      
    }
    void Shoot()

{

    Instantiate(bulletPrefab, firePointLeft.position, firePointLeft.rotation); 

    Instantiate(bulletPrefab, firePointRight.position, firePointRight.rotation); 
    Instantiate(bulletPrefab, firePointRightUp.position, firePointRightUp.rotation); 
    Instantiate(bulletPrefab, firePointRightDown.position, firePointRightDown.rotation); 
    Instantiate(bulletPrefab, firePointLeftUp.position, firePointLeftUp.rotation); 
    
}
}
