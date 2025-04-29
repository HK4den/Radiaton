using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class GunManager : MonoBehaviour
{
    public WeaponData data;                 // Assigned by WeaponManager when instantiating
    private int currentAmmo;
    private float fireTimer;
    private bool shotBuffered;

    private List<Transform> firePoints = new List<Transform>();
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        // Gather all child FirePoint markers
        foreach (var fp in GetComponentsInChildren<FirePoint>())
            firePoints.Add(fp.transform);

        if (firePoints.Count == 0)
            Debug.LogWarning($"{data.weaponName} prefab has no FirePoints!");
    }

    public void Initialize()
    {
        currentAmmo = data.maxAmmo;
        fireTimer = 0f;
        shotBuffered = false;
    }

    void Update()
    {
        // Buffer logic & firing
        bool fireInput = data.automatic
            ? Input.GetMouseButton(0)
            : Input.GetMouseButtonDown(0);

        if (fireInput)
        {
            if (fireTimer <= 0f && currentAmmo > 0)
            {
                Fire();
                fireTimer = data.fireRate;
            }
            else if (fireTimer > 0f)
            {
                shotBuffered = true;
            }
        }

        if (fireTimer > 0f)
        {
            fireTimer -= Time.deltaTime;
            if (fireTimer <= 0f && shotBuffered && currentAmmo > 0)
            {
                Fire();
                fireTimer = data.fireRate;
                shotBuffered = false;
            }
        }
    }

    void Fire()
    {
        // No ammo click
        if (currentAmmo <= 0)
        {
            audioSource.PlayOneShot(data.emptyClickSound);
            return;
        }

        // Spawn projectiles or hitscan
        foreach (var fp in firePoints)
        {
            if (data.weaponType == WeaponType.Projectile)
            {
                Instantiate(data.bulletPrefab, fp.position, fp.rotation);
            }
            else // Sniper hitscan
            {
                StartCoroutine(FireSniper(fp));
            }
        }

        currentAmmo--;
        audioSource.PlayOneShot(data.fireSound);

        // Screen shake
        if (Camera.main.TryGetComponent<CameraFollow>(out var cam))
            cam.Shake(data.shakeIntensity, data.shakeDuration);
    }

    private IEnumerator FireSniper(Transform fp)
    {
        var line = Camera.main.GetComponent<LineRenderer>();
        if (line == null) yield break;

        line.enabled = true;
        line.positionCount = 2;
        line.SetPosition(0, fp.position);

        var hit = Physics2D.Raycast(fp.position, fp.up, data.range);
        Vector3 end = hit ? hit.point : fp.position + fp.up * data.range;
        line.SetPosition(1, end);

        if (hit.collider != null && hit.collider.TryGetComponent<UnitHealthHolder>(out var h))
            h.TakeDamage(data.damage);

        yield return new WaitForSeconds(0.05f);
        line.enabled = false;
    }

    public void Reload()
    {
        currentAmmo = data.maxAmmo;
        audioSource.PlayOneShot(data.reloadSound);
    }

    public int GetCurrentAmmo() => currentAmmo;
}
