using UnityEngine;

public class ImmediateDestroy : MonoBehaviour
{
    // If you want to delay destruction (for example, waiting for an animation),
    // you can expose a delay variable. For immediate destruction, leave it at 0.
    public float destroyDelay = 0f;

    void Start()
    {
        Destroy(gameObject, destroyDelay);
    }

    // Alternatively, you can call this method from another script:
    public void DestroyNow()
    {
        Destroy(gameObject);
    }
}
