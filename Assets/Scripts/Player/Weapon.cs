using System;
using UnityEngine;
using UnityEngine.InputSystem;
public class Weapon : MonoBehaviour
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private float shootForce;
    [SerializeField] private GameObject bulletPrefab;

    [SerializeField] private bool isSemiAuto = false;

    [SerializeField] private float shootDelay = 0;
    private float nextShootTime;

    private InputAction shootAction;
    private InputAction reloadAction;

    [Header("Ammo Settings")]
    [SerializeField] private int maxAmmo = 30;
    [SerializeField] private int currentAmmo;
    [SerializeField] private float reloadTime = 2;
    private bool isReloading = false;


    [Header("Recoil Settings")]
    [SerializeField] private float recoilKickBack = 0.08f;
    [SerializeField] private float recoilKickUp = 6;
    [SerializeField] private float recoilReturnSpeed = 8;

    private Vector3 startPos;
    private Quaternion startRotation;

    private void Start()
    {
        // Get the shoot action from the Input System
        shootAction = InputSystem.actions.FindAction("Attack");
        shootAction.started += OnAttackButtonPressed;

        // Set starting ammo
        currentAmmo = maxAmmo;

        // Save the weapons starting position and rotation
        startPos = transform.localPosition;
        startRotation = transform.localRotation;
    }

    private void Update()
    {
        
        
        // Check if the weapon is semi-automatic and the shoot action is pressed
        if (!isSemiAuto && shootAction.IsPressed())
        {
            Shoot();
        }

        // Move the weapon back to its starting position and rotation
        transform.localPosition = Vector3.Lerp(transform.localPosition, startPos, recoilReturnSpeed * Time.deltaTime);
        transform.localRotation = Quaternion.Lerp(transform.localRotation, startRotation, recoilReturnSpeed * Time.deltaTime);
    }

    private void OnAttackButtonPressed(InputAction.CallbackContext context)
    {
        // Only shoot once per button press if the weapon is semi-automatic
        if (isSemiAuto)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        // Check if the weapon can shoot again based on the shoot delay
        if (Time.time < nextShootTime)
        {
            return;
        }

        // Set the time for the next shot
        nextShootTime = Time.time + shootDelay;
        
        // Create a new bullet
        GameObject newBullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

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
}
