using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Kontroler przetwarzaj¹cy dane wejœciowe od gracza
/// </summary>
public class PlayerInputController : MonoBehaviour
{
    /// <summary>
    /// Referencja do aktualnie przypisanego efektu wizualnego
    /// </summary>
    [SerializeField] private VisionModifier currentVisionModifier;

    private void Update()
    {
        if (Keyboard.current != null && Keyboard.current.gKey.wasPressedThisFrame)
        {
            if (currentVisionModifier != null)
            {
                currentVisionModifier.ToggleEffect();
            }
        }
    }

    private PlayerStatsUI playerStatsUI;

    public void SetStatsUI(PlayerStatsUI ui)
    {
        playerStatsUI = ui;
    }

    public void OnToggleStats(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;

        playerStatsUI.Toggle();
    }
}