using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aichase : MonoBehaviour
{
    public string targetTag = "Player"; // Tag to chase
    public float speed;
    public float distanceBetween;

    private GameObject target;
    private float distance;

    // Start is called before the first frame update
    void Start()
    {
        // Find the target GameObject with the specified tag
        target = GameObject.FindGameObjectWithTag(targetTag);
    }

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