using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // The character to follow

    private Vector3 offset;

    void Start()
    {
        // Calculate the initial offset between the camera and the target
        offset = transform.position - target.position;
    }

    void LateUpdate()
    {
        // Maintain the camera's position relative to the target
        transform.position = target.position + offset;

        // Reset the camera's rotation to default
        transform.rotation = Quaternion.identity;
    }
}
