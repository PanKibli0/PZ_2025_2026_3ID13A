using UnityEngine;

/// <summary>
/// Abstrakcyjna klasa bazowa dla modyfikator�w efekt�w wizualnych kamery
/// </summary>
public abstract class VisionModifier : MonoBehaviour
{
    /// <summary>
    /// Okre�lenie czy dany efekt jest obecnie aktywny
    /// </summary>
    protected bool isEffectActive = false;

    /// <summary>
    /// Prze��cza stan aktywno�ci modyfikatora na przeciwny
    /// </summary>
    public virtual void ToggleEffect()
    {
        isEffectActive = !isEffectActive;
    }

        public void SetEffectActive(bool active)
    {
        isEffectActive = active;
    }

    /// <summary>
    /// Metoda odpowiedzialna za od�wie�anie logiki i wygl�du efektu w ka�dej klatce
    /// </summary>
    protected abstract void UpdateVisuals();
}