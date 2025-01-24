using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class aichase : MonoBehaviour
{

    [SerializeField] private float countdown;
    public GameObject player;
    public float speed;
    public float distanceBetween;
   private float distance; 
 private WaveSpawner waveSpawner;
    // Start is called before the first frame update
    void Start()
    {
        waveSpawner = GetComponentInParent<WaveSpawner>();
   
       // Find the target GameObject with the specified tag
        target = GameObject.FindGameObjectWithTag(targetTag);

    
    }

    // Update is called once per frame

    void update()
    {
        countdown -= Time.deltaTime;

        if (countdown <= 0)
        {
            Destroy(gameObject);
            waveSpawner.waves[waveSpawner.currentWaveIndex].enemiesLeft--;
        }
    }
    public string targetTag = "Player"; // Tag to chase


    private GameObject target;




    // Update is called once per frame
    void Update()
    {
        // Check if the target exists before proceeding
        if (target == null)
        {
            Debug.LogWarning("Target with tag " + targetTag + " not found!");
            return;
        }

        distance = Vector2.Distance(transform.position, target.transform.position);
        Vector2 direction = target.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (distance > distanceBetween)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, target.transform.position, speed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(Vector3.forward * angle);
        }
    }
     
    }

