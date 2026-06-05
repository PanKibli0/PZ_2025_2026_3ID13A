using UnityEngine;
using UnityEngine.InputSystem;

public class StaticTVController : MonoBehaviour
{
    [Header("Ustawienia Shadera")]
    [SerializeField] private Material tvMaterial;
    [SerializeField] private float signalInterferenceSpeed = 2f;

    [Header("Ustawienia Inputu / Wyzwalaczy")]
    [SerializeField] private Key toggleKey = Key.Space;

    [Header("Stan Obiektu")]
    [SerializeField] private bool isTvOn = true;

    private float targetIntensity = 1f;

    public bool IsTvOn
    {
        get { return isTvOn; }
        private set { isTvOn = value; }
    }

    private void Start()
    {
        if (tvMaterial == null)
        {
            enabled = false;
            return;
        }
        UpdateShaderState();
    }

    private void Update()
    {
        if (Keyboard.current != null && Keyboard.current[toggleKey].wasPressedThisFrame)
        {
            ToggleTV();
        }

        if (IsTvOn)
        {
            SimulateDynamicSignal();
        }
    }

    public void ToggleTV()
    {
        IsTvOn = !IsTvOn;
        UpdateShaderState();
    }

    private void UpdateShaderState()
    {
        float currentIntensity = IsTvOn ? targetIntensity : 0f;
        tvMaterial.SetFloat("_Intensity", currentIntensity);
    }

    private void SimulateDynamicSignal()
    {
        float dynamicSpeed = Mathf.PingPong(Time.time * signalInterferenceSpeed, 50f) + 5f;
        tvMaterial.SetFloat("_Speed", dynamicSpeed);
    }
}