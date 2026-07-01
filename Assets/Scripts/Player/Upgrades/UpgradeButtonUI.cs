using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButtonUI : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_Text description;
    private UpgradeData currentUpgrade;
    private UpgradeManager manager;

    public void Setup(UpgradeData data, UpgradeManager upgradeManager)
    {
        currentUpgrade = data;
        manager = upgradeManager;

        title.text = data.upgradeName;
        description.text = data.description;
        icon.sprite = data.icon;
    }

    public void OnClick()
    {
        currentUpgrade.Apply(FindFirstObjectByType<PlayerExperience>().gameObject);

        manager.CloseUpgradeMenu();
    }
}