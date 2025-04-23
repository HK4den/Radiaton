using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponManager : MonoBehaviour
{
    [Header("Inventory")]
    public List<WeaponData> availableWeapons;
    private int currentWeaponIndex = 0;
    private Dictionary<WeaponData, int> currentAmmo = new Dictionary<WeaponData, int>();

    [Header("Reload Settings")]
    public float reloadDuration = 3f;
    private bool isReloading = false;
    private Coroutine reloadRoutine;

    [Header("References")]
    public Transform weaponParent;    // Where weapon prefab attaches
    public GameObject weaponDisplay;  // SpriteRenderer on this object will change
    public TMP_Text ammoDisplay;
    public AudioSource audioSource;
    public LineRenderer sniperLine;

    private GameObject currentWeaponObject;
    private List<Transform> firePoints = new List<Transform>();
    private float fireTimer = 0f;

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
        {
            if (Input.GetKeyDown((i + 1).ToString()))
            {
                EquipWeapon(i); return;
            }
        }
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll > 0f) EquipWeapon((currentWeaponIndex + 1) % availableWeapons.Count);
        else if (scroll < 0f) EquipWeapon((currentWeaponIndex - 1 + availableWeapons.Count) % availableWeapons.Count);
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
        UpdateAmmoUI();
    }

    void GatherFirePoints()
    {
        firePoints.Clear();
        foreach (var fp in currentWeaponObject.GetComponentsInChildren<Transform>())
        {
            if (fp.CompareTag("FirePoint")) firePoints.Add(fp);
        }
    }

    void HandleFiring()
    {
        var w = availableWeapons[currentWeaponIndex];
        bool fireInput = w.automatic ? Input.GetMouseButton(0) : Input.GetMouseButtonDown(0);
        if (fireInput && fireTimer <= 0f && currentAmmo[w] > 0)
        {
            foreach (var fp in firePoints)
            {
                if (w.weaponType == WeaponType.Projectile && w.bulletPrefab != null)
                    Instantiate(w.bulletPrefab, fp.position, fp.rotation);
                else if (w.weaponType == WeaponType.Sniper)
                    StartCoroutine(FireSniper(fp, w));
            }
            currentAmmo[w]--;
            audioSource.PlayOneShot(w.fireSound);
            UpdateAmmoUI();
            fireTimer = w.fireRate;
        }
        if (fireTimer > 0f) fireTimer -= Time.deltaTime;
    }

    IEnumerator FireSniper(Transform fp, WeaponData w)
    {
        sniperLine.enabled = true;
        sniperLine.SetPosition(0, fp.position);
        var hit = Physics2D.Raycast(fp.position, fp.up, w.range);
        Vector3 end = hit ? hit.point : fp.position + fp.up * w.range;
        sniperLine.SetPosition(1, end);
        if (hit)
        {
            var health = hit.collider.GetComponent<UnitHealthHolder>();
            if (health != null) health.TakeDamage((int)w.damage);
        }
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
        audioSource.PlayOneShot(availableWeapons[currentWeaponIndex].reloadSound);
        ammoDisplay.text = "Reloading...";
        yield return new WaitForSeconds(reloadDuration);
        foreach (var w in availableWeapons) currentAmmo[w] = w.maxAmmo;
        isReloading = false;
        UpdateAmmoUI();
    }

    public void UpdateAmmoUI()
    {
        var w = availableWeapons[currentWeaponIndex];
        ammoDisplay.text = $"{currentAmmo[w]} / {w.maxAmmo}";
    }
}
