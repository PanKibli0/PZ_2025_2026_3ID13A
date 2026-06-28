using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private TMP_Text healthText;
    private Health playerHealth;

    public void Init(Health health)
    {
        if (playerHealth != null)
        {
            playerHealth.OnHealthChanged -= UpdateHealthUI;
            playerHealth.OnDeath -= OnPlayerDeath;
        }

        playerHealth = health;

        if (playerHealth == null)
            return;

        playerHealth.OnHealthChanged += UpdateHealthUI;
        playerHealth.OnDeath += OnPlayerDeath;

        UpdateHealthUI(playerHealth.currentHealth, playerHealth.maxHealth);
    }

    private void UpdateHealthUI(float current, int max)
    {
        healthText.text = $"HP: {Mathf.RoundToInt(current)} / {max}";
    }

    private void OnPlayerDeath()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnDestroy()
    {
        if (playerHealth != null)
        {
            playerHealth.OnHealthChanged -= UpdateHealthUI;
            playerHealth.OnDeath -= OnPlayerDeath;
        }
    }
}
