using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField] private GameObject upgradePanel;

    [SerializeField] private List<UpgradeData> allUpgrades;
    [SerializeField] private UpgradeButtonUI[] buttons;
    private void OnEnable()
    {
        EventBus.OnLevelUp += OpenUpgradeMenu;
    }

    private void OnDisable()
    {
        EventBus.OnLevelUp -= OpenUpgradeMenu;
    }

    private void OpenUpgradeMenu()
    {
        Time.timeScale = 0f;
        upgradePanel.SetActive(true);

        ShowRandomUpgrades();
    }

    public void CloseUpgradeMenu()
    {
        upgradePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    private void ShowRandomUpgrades()
    {
        List<UpgradeData> pool = new List<UpgradeData>(allUpgrades);

        for (int i = 0; i < buttons.Length; i++)
        {
            int index = Random.Range(0, pool.Count);

            buttons[i].Setup(pool[index], this);

            pool.RemoveAt(index);
        }
    }
}