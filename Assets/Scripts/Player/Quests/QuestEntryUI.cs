using TMPro;
using UnityEngine;

public class QuestEntryUI : MonoBehaviour
{
    [SerializeField] private TMP_Text questNameText;
    [SerializeField] private TMP_Text progressText;

    public void Refresh(Quest quest)
    {
        questNameText.text = quest.Data.questName;
        progressText.text = $"{quest.CurrentAmount}/{quest.Data.requiredAmount}";

        if (quest.IsCompleted)
        {
            questNameText.color = Color.green;
            progressText.color = Color.green;
        }
        else
        {
            questNameText.color = Color.white;
            progressText.color = Color.white;
        }
    }
}