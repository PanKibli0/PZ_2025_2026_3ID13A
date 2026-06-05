using UnityEngine;

/// <summary>
/// Abstrakcyjna klasa bazowa dla modyfikatorów efektów wizualnych kamery
/// </summary>
public abstract class VisionModifier : MonoBehaviour
{
    /// <summary>
    /// Okreœlenie czy dany efekt jest obecnie aktywny
    /// </summary>
    protected bool isEffectActive = false;

    /// <summary>
    /// Prze³¹cza stan aktywnoœci modyfikatora na przeciwny
    /// </summary>
    public virtual void ToggleEffect()
    {
        isEffectActive = !isEffectActive;
    }

    /// <summary>
    /// Metoda odpowiedzialna za odœwie¿anie logiki i wygl¹du efektu w ka¿dej klatce
    /// </summary>
    protected abstract void UpdateVisuals();
}