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
    private GameObject player;

    public void Setup(UpgradeData data, UpgradeManager upgradeManager, GameObject playerObject)
    {
        currentUpgrade = data;
        manager = upgradeManager;
        player = playerObject;
        title.text = data.upgradeName;
        description.text = data.description;
        icon.sprite = data.icon;

    }

    public void OnClick()
    {
        currentUpgrade.Apply(player);

        manager.CloseUpgradeMenu();
    }
}