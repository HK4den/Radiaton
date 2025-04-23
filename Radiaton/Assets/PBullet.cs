using UnityEngine;

public class PBullet : MonoBehaviour
{
    [Range(1, 50)]
    [SerializeField] private float speed = 30f;
    public float damage = 10f; // Set this (or assign via the weapon) if needed.
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rb.velocity = transform.up * speed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        UnitHealthHolder health = collision.collider.GetComponent<UnitHealthHolder>();
        if (health != null)
        {
            health.TakeDamage((int)damage);
        }
        Destroy(gameObject);
    }
}
