using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spinershooting : MonoBehaviour
{
    
   

    private float timer;
    // Start is called before the first frame update
    
public GameObject bulletPrefab;
public Transform firePointLeft;
public Transform firePointRight;
public Transform firePointLeftUp;
public Transform firePointLeftDown;
public Transform firePointRightUp;
public Transform firePointRightDown;
public Transform firePointUp;
public Transform firePointDown;



    // Update is called once per frame
    void Update()
    {
      timer += Time.deltaTime;
        if(timer > .35)
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
    Instantiate(bulletPrefab, firePointLeftDown.position, firePointLeftDown.rotation); 
    Instantiate(bulletPrefab, firePointUp.position, firePointUp.rotation); 
    Instantiate(bulletPrefab, firePointDown.position, firePointDown.rotation); 

}
}
