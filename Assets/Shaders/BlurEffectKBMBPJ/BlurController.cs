using UnityEngine;

public class BlurController : MonoBehaviour
{
    private GlassesBlurModifier blurModifier;

    private void Awake()
    {
        blurModifier = FindFirstObjectByType<GlassesBlurModifier>();
    }

    public void EnableBlur()
    {
        if (blurModifier != null)
            blurModifier.SetEffectActive(true);
    }

    public void DisableBlur()
    {
        if (blurModifier != null)
            blurModifier.SetEffectActive(false);
    }
}