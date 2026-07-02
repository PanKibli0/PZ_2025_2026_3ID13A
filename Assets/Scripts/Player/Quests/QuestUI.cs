using System.Collections.Generic;
using UnityEngine;

public class QuestUI : MonoBehaviour
{
    [SerializeField] private QuestManager questManager;
    [SerializeField] private Transform container;
    [SerializeField] private QuestEntryUI questEntryPrefab;

    private readonly List<QuestEntryUI> entries = new();

    private void OnEnable()
    {
        questManager.OnQuestsChanged += Refresh;
    }

    private void OnDisable()
    {
        questManager.OnQuestsChanged -= Refresh;
    }

    private void Start()
    {
        Refresh();
    }

    private void Refresh()
    {
        foreach (QuestEntryUI entry in entries)
            Destroy(entry.gameObject);

        entries.Clear();

        foreach (Quest quest in questManager.ActiveQuests)
        {
            QuestEntryUI ui = Instantiate(questEntryPrefab, container);
            ui.Refresh(quest);
            entries.Add(ui);
        }
    }
}