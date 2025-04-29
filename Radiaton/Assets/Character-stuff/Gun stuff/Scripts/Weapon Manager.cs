using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponManager : MonoBehaviour
{
    [Header("Inventory")]
    public List<WeaponData> availableWeapons;
    private int currentWeaponIndex;
    private Dictionary<WeaponData, int> currentAmmo = new Dictionary<WeaponData, int>();

    [Header("Reload Settings")]
    public float reloadDuration = 3f;
    private bool isReloading;
    private Coroutine reloadRoutine;

    [Header("References")]
    public Transform weaponParent;
    public GameObject weaponDisplay;
    public TMP_Text ammoDisplay;
    public AudioSource audioSource;
    public LineRenderer sniperLine;

    private GameObject currentWeaponObject;
    private List<Transform> firePoints = new List<Transform>();
    private float fireTimer;
    private bool shotBuffered;

    void Start()
    {
        foreach (var w in availableWeapons)
            currentAmmo[w] = w.maxAmmo;
        EquipWeapon(0);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) StartReload();
        HandleWeaponSwitch();
        if (!isReloading) HandleFiring();
    }

    void HandleWeaponSwitch()
    {
        for (int i = 0; i < availableWeapons.Count; i++)
            if (Input.GetKeyDown((i + 1).ToString()))
                EquipWeapon(i);

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll > 0f)
            EquipWeapon((currentWeaponIndex + 1) % availableWeapons.Count);
        else if (scroll < 0f)
            EquipWeapon((currentWeaponIndex - 1 + availableWeapons.Count) % availableWeapons.Count);
    }

    void EquipWeapon(int index)
    {
        if (reloadRoutine != null) { StopCoroutine(reloadRoutine); isReloading = false; }
        currentWeaponIndex = index;
        var w = availableWeapons[index];

        if (currentWeaponObject != null) Destroy(currentWeaponObject);
        currentWeaponObject = Instantiate(w.weaponPrefab, weaponParent);
        weaponDisplay.GetComponent<SpriteRenderer>().sprite = w.weaponSprite;

        GatherFirePoints();
        fireTimer = 0f;
        shotBuffered = false;
        UpdateAmmoUI();
    }

    void GatherFirePoints()
    {
        firePoints.Clear();
        foreach (var fp in
            currentWeaponObject.GetComponentsInChildren<FirePoint>())
            firePoints.Add(fp.transform);
        if (firePoints.Count == 0)
            Debug.LogWarning($"Weapon {availableWeapons[currentWeaponIndex].weaponName} has no FirePoints!");
    }

    void HandleFiring()
    {
        var w = availableWeapons[currentWeaponIndex];
        bool fireInput = w.automatic
            ? Input.GetMouseButton(0)
            : Input.GetMouseButtonDown(0);

        if (fireInput)
        {
            if (fireTimer <= 0f && currentAmmo[w] > 0)
            {
                Shoot(w);
                fireTimer = w.fireRate;
            }
            else if (fireTimer > 0f)
            {
                shotBuffered = true;
            }
        }

        if (fireTimer > 0f)
        {
            fireTimer -= Time.deltaTime;
            if (fireTimer <= 0f && shotBuffered && currentAmmo[w] > 0)
            {
                Shoot(w);
                fireTimer = w.fireRate;
                shotBuffered = false;
            }
        }
    }

    void Shoot(WeaponData w)
    {
        foreach (var fp in firePoints)
        {
            if (w.weaponType == WeaponType.Projectile && w.bulletPrefab != null)
                Instantiate(w.bulletPrefab, fp.position, fp.rotation);
            else if (w.weaponType == WeaponType.Sniper)
                StartCoroutine(FireSniper(fp, w));
        }

        currentAmmo[w]--;
        audioSource.PlayOneShot(
            currentAmmo[w] > 0 ? w.fireSound : w.emptyClickSound
        );

        if (Camera.main.TryGetComponent<CameraFollow>(out var cam))
            cam.Shake(w.shakeIntensity, w.shakeDuration);

        UpdateAmmoUI();
    }

    IEnumerator FireSniper(Transform fp, WeaponData w)
    {
        sniperLine.enabled = true;
        sniperLine.SetPosition(0, fp.position);
        RaycastHit2D hit = Physics2D.Raycast(fp.position, fp.up, w.range);
        Vector3 end = hit ? hit.point : fp.position + fp.up * w.range;
        sniperLine.SetPosition(1, end);

        if (hit.collider != null)
            if (hit.collider.TryGetComponent<UnitHealthHolder>(out var h))
                h.TakeDamage(w.damage);

        yield return new WaitForSeconds(0.05f);
        sniperLine.enabled = false;
    }

    void StartReload()
    {
        if (isReloading) return;
        reloadRoutine = StartCoroutine(ReloadRoutine());
    }

    IEnumerator ReloadRoutine()
    {
        isReloading = true;
        var w = availableWeapons[currentWeaponIndex];
        audioSource.PlayOneShot(w.reloadSound);
        ammoDisplay.text = "Reloading...";
        yield return new WaitForSeconds(reloadDuration);

        foreach (var wep in availableWeapons)
            currentAmmo[wep] = wep.maxAmmo;

        isReloading = false;
        UpdateAmmoUI();
    }

    void UpdateAmmoUI()
    {
        var w = availableWeapons[currentWeaponIndex];
        ammoDisplay.text = currentAmmo[w] > 0
            ? $"{currentAmmo[w]} / {w.maxAmmo}"
            : $"0 / {w.maxAmmo}";
    }
}
