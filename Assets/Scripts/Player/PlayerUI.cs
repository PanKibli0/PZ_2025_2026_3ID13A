using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private TMP_Text healthText;
    private Health playerHealth;

    public void init(Health health)
    {
        if (playerHealth != null)
        {
            playerHealth.OnHealthChanged -= updateHealthUI;
            playerHealth.OnDeath -= onPlayerDeath;
        }

        playerHealth = health;

        if (playerHealth == null)
            return;

        playerHealth.OnHealthChanged += updateHealthUI;
        playerHealth.OnDeath += onPlayerDeath;

        updateHealthUI(playerHealth.CurrentHealth, playerHealth.MaxHealth);
    }

    private void updateHealthUI(int current, int max)
    {
        healthText.text = $"HP: {current} / {max}";
    }

    private void onPlayerDeath()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // DEBUG
    }

    private void OnDestroy()
    {
        if (playerHealth != null)
        {
            playerHealth.OnHealthChanged -= updateHealthUI;
            playerHealth.OnDeath -= onPlayerDeath;
        }
    }
}
    // TODO: DEATH LOGIC elsewhere