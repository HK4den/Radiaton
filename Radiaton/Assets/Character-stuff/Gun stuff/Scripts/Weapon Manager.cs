using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponManager : MonoBehaviour
{
    [Header("Weapon Inventory")]
    [Tooltip("Drag your weapon prefabs (with GunManager) here")]
    public List<GameObject> weaponPrefabs;

    private int currentIndex;
    private GunManager currentGun;

    [Header("Reload")]
    public float reloadDuration = 3f;
    private bool isReloading;
    private Coroutine reloadCoroutine;

    [Header("UI & References")]
    public Transform weaponParent;     // where the gun prefab will be instantiated
    public SpriteRenderer weaponDisplay;// UI icon
    public TMP_Text ammoDisplay;       // ammo text
    public AudioSource audioSource;    // for reload SFX
    public LineRenderer sniperLine;    // assign the scene’s LineRenderer

    void Start()
    {
        EquipWeapon(0);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && !isReloading)
            reloadCoroutine = StartCoroutine(ReloadCurrent());

        HandleSwitchInput();
        UpdateAmmoUI();
    }

    void HandleSwitchInput()
    {
        for (int i = 0; i < weaponPrefabs.Count; i++)
        {
            if (Input.GetKeyDown((i + 1).ToString()))
                EquipWeapon(i);
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll > 0f)
            EquipWeapon((currentIndex + 1) % weaponPrefabs.Count);
        else if (scroll < 0f)
            EquipWeapon((currentIndex - 1 + weaponPrefabs.Count) % weaponPrefabs.Count);
    }

    void EquipWeapon(int index)
    {
        if (reloadCoroutine != null)
        {
            StopCoroutine(reloadCoroutine);
            isReloading = false;
        }

        if (currentGun != null)
            Destroy(currentGun.gameObject);

        currentIndex = index;
        GameObject prefab = weaponPrefabs[index];
        GameObject go = Instantiate(prefab, weaponParent);
        currentGun = go.GetComponent<GunManager>();

        // Update UI icon
        weaponDisplay.sprite = currentGun.data.weaponSprite;
    }

    IEnumerator ReloadCurrent()
    {
        isReloading = true;
        ammoDisplay.text = "Reloading...";
        audioSource.PlayOneShot(currentGun.data.reloadSound);

        yield return new WaitForSeconds(reloadDuration);

        currentGun.Reload();
        isReloading = false;
        UpdateAmmoUI();
    }

    void UpdateAmmoUI()
    {
        if (currentGun == null) return;
        int ammo = currentGun.GetCurrentAmmo();
        int max = currentGun.data.maxAmmo;
        ammoDisplay.text = $"{ammo} / {max}";
    }
}
