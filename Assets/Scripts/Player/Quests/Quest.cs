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

    public void AddProgress(int amount = 1)
    {
        if (IsCompleted)
            return;

        CurrentAmount += amount;

        if (CurrentAmount > Data.requiredAmount)
            CurrentAmount = Data.requiredAmount;
    }
    public void SetProgress(int value)
    {
        CurrentAmount = Mathf.Min(value, Data.requiredAmount);
    }
    public bool RewardClaimed { get; private set; }

    public void ClaimReward()
    {
        RewardClaimed = true;
    }
}