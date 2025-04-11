using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponManager : MonoBehaviour
{
    [Header("Weapon Setup")]
    public List<WeaponData> availableWeapons; // List of weapon assets
    private int currentWeaponIndex = 0;

    // Store the current ammo for each weapon at runtime.
    private Dictionary<WeaponData, int> currentAmmoDict = new Dictionary<WeaponData, int>();

    [Header("References")]
    public Transform firingPoint;          // Where bullets or the sniper ray originate
    public TMP_Text ammoDisplay;           // TMP text to show ammo or "Reloading"
    public AudioSource audioSource;        // For playing firing and reload sounds
    public LineRenderer sniperLine;        // For drawing the sniper shot line (disabled by default)

    [Header("Shooting State")]
    private bool isReloading = false;
    private float fireTimer = 0f;

    void Start()
    {
        // Initialize the ammo for all weapons.
        foreach (var weapon in availableWeapons)
        {
            currentAmmoDict[weapon] = weapon.maxAmmo;
        }
        UpdateAmmoDisplay();
        EquipWeapon(currentWeaponIndex);

        if (sniperLine != null)
            sniperLine.enabled = false;
    }

    void Update()
    {
        HandleWeaponSwitchInput();
        if (isReloading)
            return;

        HandleFireInput();
        HandleReloadInput();
    }

    void HandleWeaponSwitchInput()
    {
        // Switch using number keys (e.g., key "1" selects weapon at index 0)
        for (int i = 0; i < availableWeapons.Count; i++)
        {
            // Using string conversion for simplicity
            if (Input.GetKeyDown((i + 1).ToString()))
            {
                EquipWeapon(i);
            }
        }

        // Switch using mouse scroll wheel
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll > 0f)
        {
            int newIndex = currentWeaponIndex + 1;
            if (newIndex >= availableWeapons.Count) newIndex = 0;
            EquipWeapon(newIndex);
        }
        else if (scroll < 0f)
        {
            int newIndex = currentWeaponIndex - 1;
            if (newIndex < 0) newIndex = availableWeapons.Count - 1;
            EquipWeapon(newIndex);
        }
    }

    void EquipWeapon(int index)
    {
        currentWeaponIndex = index;
        UpdateAmmoDisplay();
    }

    void HandleFireInput()
    {
        WeaponData currentWeapon = availableWeapons[currentWeaponIndex];
        bool fireInput = false;
        if (currentWeapon.automatic)
            fireInput = Input.GetMouseButton(0);
        else
            fireInput = Input.GetMouseButtonDown(0);

        if (fireInput && fireTimer <= 0f)
        {
            if (currentAmmoDict[currentWeapon] > 0)
            {
                FireWeapon(currentWeapon);
                currentAmmoDict[currentWeapon]--;
                UpdateAmmoDisplay();
                fireTimer = currentWeapon.fireRate;
            }
            else
            {
                // Optionally, play an "empty" sound here.
            }
        }
        if (fireTimer > 0f)
        {
            fireTimer -= Time.deltaTime;
        }
    }

    void FireWeapon(WeaponData weapon)
    {
        if (weapon.weaponType == WeaponType.Projectile)
        {
            // Spawn the bullet prefab at the firing point.
            if (weapon.bulletPrefab != null)
                Instantiate(weapon.bulletPrefab, firingPoint.position, firingPoint.rotation);
        }
        else if (weapon.weaponType == WeaponType.Sniper)
        {
            // Perform a raycast for sniper weapon.
            RaycastHit2D hit = Physics2D.Raycast(firingPoint.position, firingPoint.up, weapon.range);
            if (sniperLine != null)
            {
                sniperLine.enabled = true;
                sniperLine.SetPosition(0, firingPoint.position);
                if (hit.collider != null)
                {
                    sniperLine.SetPosition(1, hit.point);
                    // If the hit object has a UnitHealthHolder, apply damage.
                    UnitHealthHolder enemyHealth = hit.collider.GetComponent<UnitHealthHolder>();
                    if (enemyHealth != null)
                    {
                        enemyHealth.TakeDamage((int)weapon.damage);
                    }
                }
                else
                {
                    sniperLine.SetPosition(1, firingPoint.position + firingPoint.up * weapon.range);
                }
                StartCoroutine(DisableSniperLine());
            }
        }

        // Play firing sound.
        if (audioSource != null && weapon.fireSound != null)
            audioSource.PlayOneShot(weapon.fireSound);
    }

    IEnumerator DisableSniperLine()
    {
        yield return new WaitForSeconds(0.1f);
        if (sniperLine != null)
            sniperLine.enabled = false;
    }

    void HandleReloadInput()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(ReloadAllWeapons());
        }
    }

    IEnumerator ReloadAllWeapons()
    {
        isReloading = true;
        ammoDisplay.text = "Reloading...";
        WeaponData currentWeapon = availableWeapons[currentWeaponIndex];

        // Play reload sound.
        if (audioSource != null && currentWeapon.reloadSound != null)
            audioSource.PlayOneShot(currentWeapon.reloadSound);

        // Wait for the reload time (using the active weapon's reload time).
        yield return new WaitForSeconds(currentWeapon.reloadTime);

        // Reload all weapons.
        foreach (var weapon in availableWeapons)
        {
            currentAmmoDict[weapon] = weapon.maxAmmo;
        }
        isReloading = false;
        UpdateAmmoDisplay();
    }

    void UpdateAmmoDisplay()
    {
        WeaponData currentWeapon = availableWeapons[currentWeaponIndex];
        ammoDisplay.text = currentAmmoDict[currentWeapon] + " / " + currentWeapon.maxAmmo;
    }
}
