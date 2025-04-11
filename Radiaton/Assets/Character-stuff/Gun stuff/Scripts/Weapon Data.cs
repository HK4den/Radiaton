using UnityEngine;

public enum WeaponType
{
    Projectile,
    Sniper
}

[CreateAssetMenu(fileName = "NewWeaponData", menuName = "Weapon Data")]
public class WeaponData : ScriptableObject
{
    public string weaponName;
    public WeaponType weaponType;
    [Header("Ammo & Fire Settings")]
    public int maxAmmo = 30;
    public float fireRate = 0.5f; // seconds between shots
    public bool automatic = true;
    public float reloadTime = 2f; // seconds to reload
    [Header("Damage & Range")]
    public float damage = 10f;
    // For sniper weapons
    public float range = 50f;
    [Header("Prefabs & Sounds")]
    public GameObject bulletPrefab; // only used when WeaponType is Projectile
    public AudioClip fireSound;
    public AudioClip reloadSound;
}
