using UnityEngine;

public class UnitHealthHolder : MonoBehaviour
{
    public UnitHealth unitHealth;

    void Awake()
    {
        // If no health has been assigned in the Inspector, initialize to a default value.
        if (unitHealth == null)
        {
            unitHealth = new UnitHealth(10, 10);
        }
    }

    public void TakeDamage(int damage)
    {
        unitHealth.DmgUnit(damage);
        if (unitHealth.Health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
