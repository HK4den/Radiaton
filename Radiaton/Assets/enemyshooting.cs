using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossshooting : MonoBehaviour
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
public Transform firePoint1;
public Transform firePoint2;
public Transform firePoint3;
public Transform firePoint4;
public Transform firePoint5;
public Transform firePoint6;
public Transform firePoint7;
public Transform firePoint8;
public Transform firePoint9;
public Transform firePoint10;
public Transform firePoint11;
public Transform firePoint12;
public Transform firePoint13;
public Transform firePoint14;
public Transform firePoint15;
public Transform firePoint16;
public Transform firePoint17;
public Transform firePoint18;
public Transform firePoint19;
public Transform firePoint20;
public Transform firePoint21;
public Transform firePoint22;
public Transform firePoint23;
public Transform firePoint24;
public Transform firePoint25;
public Transform firePoint26;
public Transform firePoint27;
public Transform firePoint28;



    // Update is called once per frame
    void Update()
    {
      timer += Time.deltaTime;
        if(timer > 1.75)
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

    Instantiate(bossbulletPrefab, firePoint1.position, firePoint1.rotation); 
    Instantiate(bossbulletPrefab, firePoint2.position, firePoint2.rotation); 
Instantiate(bossbulletPrefab, firePoint3.position, firePoint3.rotation); 
    Instantiate(bossbulletPrefab, firePoint4.position, firePoint4.rotation); 
Instantiate(bossbulletPrefab, firePoint5.position, firePoint5.rotation); 
    Instantiate(bossbulletPrefab,  firePoint6.position,  firePoint6.rotation); 
    Instantiate(bossbulletPrefab, firePoint7.position, firePoint7.rotation); 
    Instantiate(bossbulletPrefab,  firePoint8.position, firePoint8.rotation); 
    Instantiate(bossbulletPrefab, firePoint9.position, firePoint9.rotation); 
    Instantiate(bossbulletPrefab, firePoint10.position, firePoint10.rotation); 
    Instantiate(bossbulletPrefab, firePoint11.position, firePoint11.rotation); 
    Instantiate(bossbulletPrefab, firePoint12.position, firePoint12.rotation); 
    Instantiate(bossbulletPrefab, firePoint13.position, firePoint13.rotation); 
    Instantiate(bossbulletPrefab, firePoint14.position, firePoint14.rotation); 
    Instantiate(bossbulletPrefab,  firePoint15.position, firePoint15.rotation); 
    Instantiate(bossbulletPrefab, firePoint16.position, firePoint16.rotation); 
    Instantiate(bossbulletPrefab, firePoint17.position, firePoint17.rotation); 
    Instantiate(bossbulletPrefab, firePoint18.position, firePoint18.rotation); 
    Instantiate(bossbulletPrefab, firePoint19.position, firePoint19.rotation); 
      Instantiate(bossbulletPrefab, firePoint20.position, firePoint20.rotation); 
      Instantiate(bossbulletPrefab, firePoint21.position, firePoint21.rotation);  
Instantiate(bossbulletPrefab, firePoint22.position, firePoint22.rotation); 
Instantiate(bossbulletPrefab, firePoint23.position, firePoint23.rotation); 
Instantiate(bossbulletPrefab, firePoint24.position, firePoint24.rotation); 
Instantiate(bossbulletPrefab, firePoint25.position, firePoint25.rotation); 
Instantiate(bossbulletPrefab, firePoint26.position, firePoint26.rotation); 
Instantiate(bossbulletPrefab, firePoint27.position, firePoint27.rotation); 
Instantiate(bossbulletPrefab, firePoint28.position, firePoint28.rotation); 

}
}
