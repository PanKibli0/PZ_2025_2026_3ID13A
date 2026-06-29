using UnityEngine;
using System.Collections.Generic;

public class StatusEffectController : MonoBehaviour
{
    private readonly List<StatusEffect> activeEffects = new();

    public void AddEffect(StatusEffect effect)
    {
        if (effect == null) return;

        foreach (var active in activeEffects)
        {
            if (active.GetType() == effect.GetType())
            {
                active.Refresh();
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