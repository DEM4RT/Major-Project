using UnityEngine;
using UnityEngine.InputSystem;
public class Weapon : MonoBehaviour
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private float shootForce;
    [SerializeField] private GameObject bulletPrefab;

    private InputAction shootAction;

    private void Start()
    {
        shootAction = InputSystem.actions.FindAction("Attack");
        shootAction.started += OnAttackButtonPressed;
    }

    private void OnAttackButtonPressed(InputAction.CallbackContext _)
    {
        Debug.Log("Shoot!");
    }

    private void OnDestroy()
    {
        shootAction.started -= OnAttackButtonPressed;
    }
}
