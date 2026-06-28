using UnityEngine;

/// <summary>
/// Implementuje efekt rozmycia obrazu symuluj¹cy wadê wzroku i brak okularów
/// </summary>
public class GlassesBlurModifier : VisionModifier
{
    /// <summary>
    /// Materia³ z przypisanym shaderem HLSL wykonuj¹cym operacje na pikselach
    /// </summary>
    [SerializeField] private Material blurMaterial;

    /// <summary>
    /// Maksymalna wartoœæ si³y rozmycia.
    /// </summary>
    [SerializeField] private float maxBlurAmount = 0.005f;

    /// <summary>
    /// Prêdkoœæ przejœcia pomiêdzy rozmytym a ostrym obrazem
    /// </summary>
    [SerializeField] private float transitionSpeed = 5f;

    private float currentBlur = 0f;

    private void Start()
    {
        isEffectActive = false;
    }

    private void Update()
    {
        UpdateVisuals();
    }

    /// <summary>
    /// Obliczanie nowej wartoœci rozmycia
    /// </summary>
    protected override void UpdateVisuals()
    {
        float targetBlur = isEffectActive ? maxBlurAmount : 0f;

        currentBlur = Mathf.MoveTowards(currentBlur, targetBlur, Time.deltaTime * transitionSpeed * maxBlurAmount);

        if (blurMaterial != null)
        {
            blurMaterial.SetFloat("_BlurStrength", currentBlur);
        }
    }

    private void OnDisable()
    {
        if (blurMaterial != null)
        {
            blurMaterial.SetFloat("_BlurStrength", 0f);
        }
    }
}