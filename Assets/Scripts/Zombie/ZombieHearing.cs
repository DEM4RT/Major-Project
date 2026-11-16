using UnityEngine;

public class ZombieHearing : MonoBehaviour
{
    [Header("Hearing Settings")]
    [SerializeField] private float hearingRange = 15;
    
    private Vector3 lastHeardPosition;
    private bool hasHeardNoise;

    public Vector3 LastHeardPosition => lastHeardPosition;
    public bool HasHeardNoise => hasHeardNoise;

    // Check wether the zombie has heard a sound and if it is within range
    public void HeardSound(Vector3 noisePosition, float noiseRange)
    {
        float distance = Vector3.Distance(transform.position, noisePosition);

        if (distance <= hearingRange && distance <= noiseRange)
        {
            lastHeardPosition = noisePosition;
            hasHeardNoise = true;
        }

        Debug.Log("Distance to noise: " + distance);
        Debug.Log("Hearing range: " + hearingRange);
        Debug.Log("Noise range: " + noiseRange);

        if (distance <= hearingRange && distance <= noiseRange)
        {
            lastHeardPosition = noisePosition;
            hasHeardNoise = true;

            Debug.Log("Zombie heard a noise!");
        }
        else
        {
            Debug.Log("Noise is too far away!");
        }
    }

    // Forget the last heard sound
    public void ResetNoise()
    {
        hasHeardNoise = false;
    }
}
