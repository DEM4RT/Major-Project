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

    [SerializeField] private float recoilKickBack = 0.08f;
    [SerializeField] private float recoilKickUp = 6;
    [SerializeField] private float recoilReturnSpeed = 8;

    private Vector3 startPos;
    private Quaternion startRotation;

    private void Start()
    {
        shootAction = InputSystem.actions.FindAction("Attack");
        shootAction.started += OnAttackButtonPressed;

        startPos = transform.localPosition;
        startRotation = transform.localRotation;
    }

    private void Update()
    {
        if (!isSemiAuto && shootAction.IsPressed())
        {
            Shoot();
        }

        transform.localPosition = Vector3.Lerp(transform.localPosition, startPos, recoilReturnSpeed * Time.deltaTime);
        transform.localRotation = Quaternion.Lerp(transform.localRotation, startRotation, recoilReturnSpeed * Time.deltaTime);
    }

    private void OnAttackButtonPressed(InputAction.CallbackContext _)
    {
        if (isSemiAuto)
        {
            Shoot();
        }
    }
    private void OnDestroy()
    {
        shootAction.started -= OnAttackButtonPressed;
    }

    private void Shoot()
    {
        if (Time.time < nextShootTime)
        {
            return;
        }

        nextShootTime = Time.time + shootDelay;
        
        GameObject newBullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        if (newBullet.TryGetComponent(out Rigidbody rb))
        {
            rb.AddForce(shootForce * firePoint.forward * Time.fixedDeltaTime, ForceMode.Impulse);
        }

        transform.localPosition -= new Vector3(0, 0, recoilKickBack);
        transform.localRotation *= Quaternion.Euler(-recoilKickUp, 0, 0);
    }
}
