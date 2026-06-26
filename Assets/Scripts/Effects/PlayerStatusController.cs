using UnityEngine;
using System.Collections.Generic;

public class PlayerStatusController : MonoBehaviour, IStatusEffectReceiver
{
    private readonly List<StatusEffect> activeEffects = new();

    public void AddEffect(StatusEffect effect)
    {
        if (effect == null)
            return;

        foreach (var activeEffect in activeEffects)
        {
            if (activeEffect.GetType() == effect.GetType())
            {
                activeEffect.Refresh();
                return;
            }
        }

        activeEffects.Add(effect);
        effect.OnApply();
    }

    private void Update()
    {
        float dt = Time.deltaTime;

        for (int i = activeEffects.Count - 1; i >= 0; i--)
        {
            activeEffects[i].Tick(dt);

            if (activeEffects[i].Finished)
            {
                activeEffects[i].OnExpire();
                activeEffects.RemoveAt(i);
            }
        }
    }
}