using System;
using System.Collections.Generic;
using UnityEngine;
using static EnemyData;

public class QuestManager : MonoBehaviour
{
    [SerializeField] private List<QuestData> questPool;
    private List<QuestData> availableQuests = new();
    [SerializeField] private int maxActiveQuests = 3;

    private readonly List<Quest> activeQuests = new();

    public event Action OnQuestsChanged;
    public IReadOnlyList<Quest> ActiveQuests => activeQuests;
    private PlayerExperience playerExperience;
    private PlayerCurrency playerCurrency;
    [SerializeField] private NotificationUI notificationUI;


    public void Init(GameObject player)
    {
        playerExperience = player.GetComponentInChildren<PlayerExperience>();
        playerCurrency = player.GetComponentInChildren<PlayerCurrency>();
    }

    private void OnEnable()
    {
        EventBus.OnEnemyKilled += HandleEnemyKilled;
    }

    private void OnDisable()
    {
        EventBus.OnEnemyKilled -= HandleEnemyKilled;
    }

    private void Start()
    {
        availableQuests = new List<QuestData>(questPool);

        for (int i = 0; i < maxActiveQuests; i++)
        {
            AddRandomQuest();
        }

        AddRandomQuest();

        OnQuestsChanged?.Invoke();
    }
    private void AddRandomQuest()
    {
        List<QuestData> candidates = new();

        foreach (QuestData quest in availableQuests)
        {
            bool alreadyActive = false;

            foreach (Quest active in activeQuests)
            {
                if (active.Data.objectiveType == quest.objectiveType &&
                    active.Data.enemyType == quest.enemyType)
                {
                    alreadyActive = true;
                    break;
                }
            }

            if (!alreadyActive)
                candidates.Add(quest);
        }

        if (candidates.Count == 0)
            return;

        int index = UnityEngine.Random.Range(0, candidates.Count);

        QuestData selected = candidates[index];

        activeQuests.Add(new Quest(selected));

        availableQuests.Remove(selected);
    }

    private void HandleEnemyKilled(EnemyType enemyType)
    {
        foreach (Quest quest in activeQuests)
        {
            if (quest.IsCompleted)
                continue;

            if (quest.Data.objectiveType != QuestObjectiveType.KillEnemy)
                continue;

            if (quest.Data.enemyType != enemyType)
                continue;

            quest.AddProgress();
            OnQuestsChanged?.Invoke();

            if (quest.IsCompleted)
                CompleteQuest(quest);

            break;
        }
    }

    private void CompleteQuest(Quest quest)
    {
        if (quest.RewardClaimed)
            return;

        if (playerCurrency != null)
            playerCurrency.AddMoney(quest.Data.moneyReward);

        if (playerExperience != null)
            playerExperience.AddExperience(quest.Data.experienceReward);

        quest.ClaimReward();

        notificationUI.Show(
            "UKOÑCZONO ZADANIE!",
            $"{quest.Data.questName}\n\n+{quest.Data.experienceReward} XP\n+${quest.Data.moneyReward}"
        );
        activeQuests.Remove(quest);

        AddRandomQuest();

        OnQuestsChanged?.Invoke();
    }
}