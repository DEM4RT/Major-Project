using System.Collections.Generic;
using UnityEngine;

public class MinimapController : MonoBehaviour
{
    [SerializeField] private GameObject zombieMarker;
    [SerializeField] private RectTransform minimap;
    [SerializeField] private Transform player;
    [SerializeField] private float mapRange = 30;

    private List<GameObject> zombies = new List<GameObject>();
    private List<RectTransform> zombieMarkers = new List<RectTransform>();

    private float enemyCheckTimer;
    private float enemyCheckDuration = 1;

    private void Start()
    {
        FindZombies();
    }

    private void Update()
    {
        enemyCheckTimer += Time.deltaTime;

        if(enemyCheckTimer >= enemyCheckDuration)
        {
            enemyCheckTimer = 0;
            FindZombies();
        }

        UpdateMarkers();
    }

    private void FindZombies()
    {
        // Find all zombies in the scene
        GameObject[] foundZombies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject zombie in foundZombies)
        {
            // Check if this zombie is already being tracked
            if (zombies.Contains(zombie))
            {
                continue;
            }
            
            // Add the zombie to the list of zombies
            zombies.Add(zombie);

            // Create a new marker for each zombie
            GameObject newMarker = Instantiate(zombieMarker, minimap);
            // Set the markers position based on the zombies position relative to the player
            zombieMarkers.Add(newMarker.GetComponent<RectTransform>());
        }
    }

    private void UpdateMarkers()
    {
        for (int i = 0; i < zombies.Count; i++)
        {
            // Check if the zombie is destroyed and disable the marker if it is
            if (zombies[i] == null)
            {
                zombieMarkers[i].gameObject.SetActive(false);
                continue;
            }

            // Calculate the direction from the player to the zombie in local space
            Vector3 direction = player.InverseTransformDirection(zombies[i].transform.position - player.position);

            // Calculate the position of the marker on the minimap
            Vector2 markerPos = new Vector2(direction.x, direction.z) / mapRange * (minimap.rect.width / 2);

            // Convert the enemies world position into the position on the minimap
            float halfWidth = minimap.rect.width / 2;
            float halfHeight = minimap.rect.height / 2;

            // Get the boundaries of the minimap
            markerPos.x = Mathf.Clamp(markerPos.x, -halfWidth, halfWidth);
            markerPos.y = Mathf.Clamp(markerPos.y, -halfHeight, halfHeight);

            // Keep markers inside the minimap
            zombieMarkers[i].anchoredPosition = markerPos;

            // Rotate the enemies ui dot
            float zombieDirection = zombies[i].transform.eulerAngles.y - player.eulerAngles.y;
            zombieMarkers[i].localRotation = Quaternion.Euler(0, 0, -zombieDirection);

        }
    }
}
