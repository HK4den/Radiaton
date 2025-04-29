using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    private Vector3 offset;

    private float shakeTimeLeft = 0f;
    private float shakeDuration = 0f;
    private float initialIntensity = 0f;

    void Start()
    {
        offset = transform.position - target.position;
    }

    public void Shake(float intensity, float duration)
    {
        if (intensity > initialIntensity)
            initialIntensity = intensity;
        if (duration > shakeTimeLeft)
            shakeTimeLeft = duration;
        shakeDuration = Mathf.Max(shakeDuration, duration);
    }

    void LateUpdate()
    {
        Vector3 basePos = target.position + offset;

        if (shakeTimeLeft > 0f)
        {
            float pct = shakeTimeLeft / shakeDuration;
            float currentIntensity = initialIntensity * pct;
            Vector2 rnd = Random.insideUnitCircle * currentIntensity;
            transform.position = basePos + new Vector3(rnd.x, rnd.y, 0f);

            shakeTimeLeft -= Time.deltaTime;
            if (shakeTimeLeft <= 0f)
            {
                shakeTimeLeft = 0f;
                initialIntensity = 0f;
                shakeDuration = 0f;
            }
        }
        else
        {
            transform.position = basePos;
        }

        transform.rotation = Quaternion.identity;
    }
}
