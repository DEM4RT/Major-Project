using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
public class Weapon : MonoBehaviour
{
    private StatsManager stats;

    [SerializeField] private Transform firePoint;
    [SerializeField] private float shootForce;
    [SerializeField] private GameObject bulletPrefab;

    [SerializeField] private bool isSemiAuto = false;

    [SerializeField] private float shootDelay = 0;
    private float nextShootTime;

    private InputAction shootAction;
    private InputAction reloadAction;

    [Header("Ammo Settings")]
    [SerializeField] private float maxAmmo = 30;
    [SerializeField] private float currentAmmo;
    [SerializeField] private float reloadTime = 2;
    [SerializeField] private TextMeshProUGUI ammoCount;
    private bool isReloading = false;
    private float reloadEndTime;

    [Header("Recoil Settings")]
    [SerializeField] private float recoilKickBack = 0.08f;
    [SerializeField] private float recoilKickUp = 6;
    [SerializeField] private float recoilReturnSpeed = 8;

    private Vector3 startPos;
    private Quaternion startRotation;

    private void Awake()
    {
        // Find the stat manager gameobject
        stats = FindAnyObjectByType<StatsManager>();
    }
    private void Start()
    {
        // Get the shoot action from the Input System
        shootAction = InputSystem.actions.FindAction("Attack");
        shootAction.started += OnAttackButtonPressed;

        // Get the reload action from the Input System
        reloadAction = InputSystem.actions.FindAction("Reload");
        reloadAction.started += OnReloadButtonPressed;

        // Set starting ammo
        currentAmmo = maxAmmo;

        // Save the weapons starting position and rotation
        startPos = transform.localPosition;
        startRotation = transform.localRotation;
    }

    private void Update()
    {
        // Check if the weapon is reloading and if the reload time has passed
        if (isReloading && Time.time >= reloadEndTime)
        {
            // Replenish the ammo
            currentAmmo = maxAmmo;
            isReloading = false;
        }

        // Check if the weapon is semi-automatic and the shoot action is pressed
        if (!isSemiAuto && shootAction.IsPressed())
        {
            Shoot();
        }

        // Move the weapon back to its starting position and rotation
        transform.localPosition = Vector3.Lerp(transform.localPosition, startPos, recoilReturnSpeed * Time.deltaTime);
        transform.localRotation = Quaternion.Lerp(transform.localRotation, startRotation, recoilReturnSpeed * Time.deltaTime);

        ammoCount.text = "Ammo count " + currentAmmo + " / 30";
    }

    private void OnAttackButtonPressed(InputAction.CallbackContext context)
    {
        // Only shoot once per button press if the weapon is semi-automatic
        if (isSemiAuto)
        {
            Shoot();
        }
    }

    private void OnReloadButtonPressed(InputAction.CallbackContext context)
    {
        Reload();
    }

    private void Shoot()
    {
        // Check if the weapon is reloading or out of ammo
        if (isReloading || currentAmmo <= 0)
        {
            return;
        }
        
        // Check if the weapon can shoot again based on the shoot delay
        if (Time.time < nextShootTime)
        {
            return;
        }

        // Set the time for the next shot
        nextShootTime = Time.time + shootDelay;

        // Decrease the current ammo count when the weapon is fired
        currentAmmo--;

        // Create a new bullet
        GameObject newBullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // Increase total shots count
        stats.TotalShots();

        // Make a noise when the weapon is fired
        NoiseManager.Instance.MakeNoise(firePoint.position, 20);

        // Add force to the bullet's Rigidbody to make it move
        Rigidbody bulletRb = newBullet.GetComponent<Rigidbody>();

        if (bulletRb != null)
        {
            Vector3 force = firePoint.forward * shootForce;
            bulletRb.AddForce(force, ForceMode.Impulse);
        }

        // Apply recoil to the weapon
        transform.localPosition -= new Vector3(0, 0, recoilKickBack);
        transform.localRotation *= Quaternion.Euler(-recoilKickUp, 0, 0);
    }

    private void Reload()
    {
        // Check if the weapon is already reloading or if the ammo is full
        if (isReloading || currentAmmo == maxAmmo)
        {
            return;
        }

        // Call reload count method
        stats.ReloadCount();

        isReloading = true;

        reloadEndTime = Time.time + reloadTime;
    }
}
