using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossshootinge : MonoBehaviour
{
    public GameObject bullet;
   

    private float timer;
    // Start is called before the first frame update
    
public GameObject bossbulletPrefab;
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

  
    Instantiate(bossbulletPrefab, firePointLeft.position, firePointLeft.rotation); 

    Instantiate(bossbulletPrefab, firePointRight.position, firePointRight.rotation); 
    Instantiate(bossbulletPrefab, firePointRightUp.position, firePointRightUp.rotation); 
    Instantiate(bossbulletPrefab, firePointRightDown.position, firePointRightDown.rotation); 
    Instantiate(bossbulletPrefab, firePointLeftUp.position, firePointLeftUp.rotation); 
    Instantiate(bossbulletPrefab, firePointLeftDown.position, firePointLeftDown.rotation); 
    Instantiate(bossbulletPrefab, firePointUp.position, firePointUp.rotation); 
    Instantiate(bossbulletPrefab, firePointDown.position, firePointDown.rotation); 

}
}

