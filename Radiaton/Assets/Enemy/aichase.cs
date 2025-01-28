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

    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    
     void update()
    {
        
    }
<<<<<<< Updated upstream
    public string targetTag = "Player"; // Tag to chase
   

    private GameObject target;
 

    
=======
>>>>>>> Stashed changes

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
   







