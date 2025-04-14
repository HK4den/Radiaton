using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shooting : MonoBehaviour
{
    [SerializeField] private GameObject PlayerBulletPrefab;
    [SerializeField] private Transform firingpoint;
    [Range(0.1f, 2f)]
    

   
    private float downFireTimer;
    [Range(0.1f, 2f)]
    [SerializeField] private float downFireRate = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && downFireTimer <= 0f)
        {
            Shoot();
        downFireTimer = downFireRate;
        }
        
        else 
        {
            
            downFireTimer -= Time.deltaTime;
        }
    }
        void Shoot()
        {
            Instantiate(PlayerBulletPrefab, firingpoint.position, firingpoint.rotation);
        }
    
}
