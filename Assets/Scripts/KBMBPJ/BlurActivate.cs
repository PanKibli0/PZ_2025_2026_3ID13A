using UnityEngine;
using UnityEngine.InputSystem;

public class CustomGlasses : MonoBehaviour
{
    [Tooltip("Material")]
    public Material blurMaterial;

    public float maxBlurAmount = 0.015f;
    public float transitionSpeed = 5f;

    private bool wearingGlasses = true;
    private float currentBlur = 0f;

    void Update()
    {
        //W³¹cz / Wy³¹cz - klawisz G
        if (Keyboard.current != null && Keyboard.current.gKey.wasPressedThisFrame)
        {
            wearingGlasses = !wearingGlasses;
        }

        float targetBlur = wearingGlasses ? 0f : maxBlurAmount;
        currentBlur = Mathf.Lerp(currentBlur, targetBlur, Time.deltaTime * transitionSpeed);

        if (blurMaterial != null)
        {
            blurMaterial.SetFloat("_BlurStrength", currentBlur);
        }
    }

    void OnDisable()
    {
        if (blurMaterial != null)
        {
            blurMaterial.SetFloat("_BlurStrength", 0f);
        }
    }
}