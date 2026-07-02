using UnityEngine;

public class Quest
{
    public QuestData Data { get; }

    public int CurrentAmount { get; private set; }

    public bool IsCompleted => CurrentAmount >= Data.requiredAmount;

    public Quest(QuestData data)
    {
        Data = data;
    }

    public void AddProgress()
    {
        if (IsCompleted)
            return;

        CurrentAmount++;
    }
}