using UnityEngine;

public class NoiseManager : MonoBehaviour
{
    public static NoiseManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    // Sends a noise signal to all zombies within range of the sound
    public void MakeNoise(Vector3 position, float soundRange)
    {
        ZombieHearing[] zombies = FindObjectsByType<ZombieHearing>(FindObjectsSortMode.None);

        foreach (ZombieHearing zombie in zombies)
        {
            zombie.HeardSound(position, soundRange);
        }
    }
}
