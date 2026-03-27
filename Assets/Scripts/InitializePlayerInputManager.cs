using UnityEngine;
using UnityEngine.InputSystem;

public class InitializePlayerInputManager : MonoBehaviour
{
    [SerializeField]
    PlayerInputManager pim;
    [SerializeField]
    GameObject playerPrefab;
    public PlayerInput player;
    public Vector3 startPosition;
    public FieldConstraint startingField;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pim.playerPrefab = playerPrefab;
        player = pim.JoinPlayer(0,-1,"Keyboard&Mouse",Keyboard.current);
        player.transform.position = startPosition;
        startingField.playerTransform = player.transform;
    }
}
