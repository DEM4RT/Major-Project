using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class StatsUI : MonoBehaviour
{
    [SerializeField] private StatsManager statsManager;
    [SerializeField] private GameObject statsPanel;

    private InputAction statsPanelAction;

    [Header("Health Stats")]
    [SerializeField] private TextMeshProUGUI currentHealthText;
    [SerializeField] private TextMeshProUGUI lowestHealthText;
    [SerializeField] private TextMeshProUGUI healingUsageText;

    [Header("Combat Stats")]
    [SerializeField] private TextMeshProUGUI shotAccuracyText;
    [SerializeField] private TextMeshProUGUI totalShotsText;
    [SerializeField] private TextMeshProUGUI shotsHitText;
    [SerializeField] private TextMeshProUGUI shotMissedText;
    [SerializeField] private TextMeshProUGUI damageDealtText;
    [SerializeField] private TextMeshProUGUI damageTakenText;
    [SerializeField] private TextMeshProUGUI reloadCountText;

    [Header("Zombie Stats")]
    [SerializeField] private TextMeshProUGUI zombiesRemainingText;
    [SerializeField] private TextMeshProUGUI zombiesSpawnedText;
    [SerializeField] private TextMeshProUGUI zombiesKilledText;
    [SerializeField] private TextMeshProUGUI zombieHitsText;

    private void Start()
    {
        // Get the stats panel action from the Input System
        statsPanelAction = InputSystem.actions.FindAction("StatsPanel");
        statsPanelAction.started += OnStatsPanelPressed;

        // Start with the stats panel UI inactive
        statsPanel.SetActive(false);
    }
    public void Update()
    {    
        // Display health stats
        currentHealthText.text = "Current Health: " + statsManager.playersCurrentHealth;
        lowestHealthText.text = "Lowest Health Reached: " + statsManager.lowestHealthReached;
        healingUsageText.text = "Amount of times healed: " + statsManager.healingUsage;

        // Display combat stats
        totalShotsText.text = "Total Shots Fired: " + statsManager.totalShots;
        shotsHitText.text = "Shots Hit: " + statsManager.shotsHit;
        shotMissedText.text = "Shots Missed: " + statsManager.shotsMissed;
        shotAccuracyText.text = "Shot Accuracy: " + statsManager.accuracy;
        damageDealtText.text = "Damage Dealth: " + statsManager.damageDealt;
        damageTakenText.text = "Damage Taken: " + statsManager.damageTaken;
        reloadCountText.text = "Amount of times reloaded: " + statsManager.reloadCount;

        // Display zombie stats
        zombiesRemainingText.text = "Zombies Remaining: " + statsManager.zombiesRemaining;
        zombiesSpawnedText.text = "Zombies Spawned: " + statsManager.zombiesSpawned;
        zombiesKilledText.text = "Zombies Killed: " + statsManager.zombiesKilled;
        zombieHitsText.text = "Zombies Successful Hits: " + statsManager.zombieHits;
    }

    private void OnStatsPanelPressed(InputAction.CallbackContext context)
    {
        // Toggle the panel
        statsPanel.SetActive(!statsPanel.activeSelf);
    }
}
