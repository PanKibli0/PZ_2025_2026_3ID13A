using UnityEngine;
using UnityEngine.InputSystem;

public class InitializePlayerInputManager : MonoBehaviour
{
    [SerializeField] private PlayerInputManager playerInputManager;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Vector3 startPosition;
    [SerializeField] private FieldConstraint startingField;

    private PlayerInput player;

    void Start()
    {
        playerInputManager.playerPrefab = playerPrefab;
        player = playerInputManager.JoinPlayer(0, -1, "Keyboard&Mouse", Keyboard.current);
        player.transform.position = startPosition;
        startingField.playerTransform = player.transform;

        player.SwitchCurrentActionMap("Player");
    }
}