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
}