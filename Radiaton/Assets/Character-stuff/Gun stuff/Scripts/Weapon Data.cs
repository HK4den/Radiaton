using UnityEngine;

public enum WeaponType { Projectile, Sniper }

[CreateAssetMenu(fileName = "NewWeaponData", menuName = "Weapon Data")]
public class WeaponData : ScriptableObject
{
    public string weaponName;

    [Header("Ammo & Damage")]
    public int maxAmmo = 30;
    public float damage = 10f;

    [Header("Firing")]
    public WeaponType weaponType;
    public float fireRate = 0.5f;
    public bool automatic = true;
    public float range = 50f; // only for Sniper

    [Header("Prefabs & SFX")]
    public GameObject bulletPrefab;      // for Projectile
    public AudioClip fireSound;
    public AudioClip reloadSound;
    public AudioClip emptyClickSound;
    public Sprite weaponSprite;          // UI icon

    [Header("Screen Shake")]
    public float shakeIntensity = 0.1f;
    public float shakeDuration = 0.1f;
}
