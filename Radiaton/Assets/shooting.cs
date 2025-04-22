using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shooting : MonoBehaviour
{
    [SerializeField] private GameObject PlayerBulletPrefab;
    [SerializeField] private Transform firingpoint;
    [SerializeField, Range(0.1f, 2f)] private float downFireRate = 0.2f;
    private float downFireTimer;
    private bool shotBuffered;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (downFireTimer <= 0f)
            {
                Shoot();
                downFireTimer = downFireRate;
            }
            else
            {
                shotBuffered = true;
            }
        }

        if (downFireTimer > 0f)
        {
            downFireTimer -= Time.deltaTime;
            if (downFireTimer <= 0f && shotBuffered)
            {
                Shoot();
                downFireTimer = downFireRate;
                shotBuffered = false;
            }
        }
    }

    void Shoot()
    {
        Instantiate(PlayerBulletPrefab, firingpoint.position, firingpoint.rotation);
    }
}
