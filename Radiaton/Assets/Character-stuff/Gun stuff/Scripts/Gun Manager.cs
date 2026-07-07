using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class GunManager : MonoBehaviour
{
    [Header("Gun Data")]
    public WeaponData data;                 // Drag in the matching WeaponData asset

    [Header("Barrel Points")]
    [Tooltip("Drag one or more empty Transforms here for bullet/spread origins")]
    public Transform[] firingPoints;

    [Header("Buffer Settings")]
    private float fireTimer;
    private bool shotBuffered;

    private int currentAmmo;
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        currentAmmo = data.maxAmmo;
    }

    void Update()
    {
        bool fireInput = data.automatic
            ? Input.GetMouseButton(0)
            : Input.GetMouseButtonDown(0);

        if (fireInput)
        {
            if (fireTimer <= 0f && currentAmmo > 0)
            {
                Shoot();
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
                Shoot();
                fireTimer = data.fireRate;
                shotBuffered = false;
            }
        }
    }

    private void Shoot()
    {
        if (currentAmmo <= 0)
        {
            audioSource.PlayOneShot(data.emptyClickSound);
            return;
        }

        foreach (var fp in firingPoints)
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

        RaycastHit2D hit = Physics2D.Raycast(fp.position, fp.up, data.range);
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
