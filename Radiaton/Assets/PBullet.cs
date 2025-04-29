using UnityEngine;

public class PBullet : MonoBehaviour
{
    [SerializeField] private float speed = 30f;
    [SerializeField] private float damage = 10f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rb.velocity = transform.up * speed;
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.collider.TryGetComponent<UnitHealthHolder>(out var h))
            h.TakeDamage(damage);
        Destroy(gameObject);
    }
}
