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
    public int maxAmmo = 30;
    public float fireRate = 0.5f;
    public bool automatic = true;
    public float damage = 10f;
    public float range = 50f;
    public GameObject bulletPrefab;
    public AudioClip fireSound;
    public AudioClip reloadSound;
    public Sprite weaponSprite;
    public GameObject weaponPrefab;
}
