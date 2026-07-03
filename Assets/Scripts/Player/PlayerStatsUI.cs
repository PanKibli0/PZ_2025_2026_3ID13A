using TMPro;
using UnityEngine;

public class PlayerStatsUI : MonoBehaviour
{
    [SerializeField] private GameObject panel;

    [SerializeField] private TMP_Text maxHealthText;
    [SerializeField] private TMP_Text damageText;
    [SerializeField] private TMP_Text attackSpeedText;
    [SerializeField] private TMP_Text moveSpeedText;
    [SerializeField] private TMP_Text critChanceText;
    [SerializeField] private TMP_Text critDamageText;
    [SerializeField] private TMP_Text dodgeText;
    [SerializeField] private TMP_Text regenText;
    [SerializeField] private TMP_Text luckText;

    private PlayerMovement playerMovement;
    private PlayerWeaponHandler playerWeaponHandler;


    private PlayerStats stats;
    private PlayerUnit player;

    public void Init(GameObject playerObject)
    {
        player = playerObject.GetComponent<PlayerUnit>();
        stats = playerObject.GetComponentInChildren<PlayerStats>();

        playerMovement = playerObject.GetComponentInChildren<PlayerMovement>();
        playerWeaponHandler = playerObject.GetComponentInChildren<PlayerWeaponHandler>();

        panel.SetActive(false);
    }

    public void Toggle()
    {
        bool isOpening = !panel.activeSelf;

        panel.SetActive(isOpening);

        if (isOpening)
        {
            Time.timeScale = 0f;
            playerMovement.CanMove = false;
            playerWeaponHandler.CanAttack = false;
            Refresh();
        }
        else
        {
            playerMovement.CanMove = true;
            playerWeaponHandler.CanAttack = true;
            Time.timeScale = 1f;
        }
    }

    private void Refresh()
    {
        maxHealthText.text = $"Zdrowie: {player.health.maxHealth}";
        damageText.text = $"Obra¿enia: x{stats.DamageMultiplier:F2}";
        attackSpeedText.text = $"Prêdkoœæ ataku: x{stats.AttackSpeedMultiplier:F2}";
        moveSpeedText.text = $"Szybkoœæ: x{stats.MoveSpeedMultiplier:F2}";

        critChanceText.text = $"Szansa krytyczna: {stats.CriticalChance:F0}%";
        critDamageText.text = $"Obra¿enia krytyczne: x{stats.CriticalDamage:F2}";

        dodgeText.text = $"Szansa na unik: {stats.DodgeChance:F0}%";
        regenText.text = $"Regeneracja: {stats.HealthRegen:F1}/s";
        luckText.text = $"Szczêœcie: {stats.Luck:F0}%";
    }
}