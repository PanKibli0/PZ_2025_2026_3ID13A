using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private UnitDataSO unitData;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private PlayerReferenceSO playerReference;

    private Vector2 moveInput;
    private float currentMoveSpeed;

    private void Awake()
    {
        if (playerReference != null)
            playerReference.playerInstance = gameObject;
    }

    private void Start()
    {
        if (unitData != null)
        {
            rb.linearDamping = unitData.linearDamping;
            currentMoveSpeed = unitData.moveSpeed;
        }
    }

    private void FixedUpdate()
    {
        if (unitData == null) return;

        Vector2 movement = moveInput;

        if (movement.sqrMagnitude > 1f)
            movement = movement.normalized;

        rb.linearVelocity = movement * currentMoveSpeed;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    // DEBUG
    public void OnDebug1(InputAction.CallbackContext context)
    {
        if (context.performed)
            setMoveSpeed(2f);
    }

    public void OnDebug2(InputAction.CallbackContext context)
    {
        if (context.performed)
            setMoveSpeed(5f);
    }

    public void OnDebug3(InputAction.CallbackContext context)
    {
        if (context.performed)
            setMoveSpeed(10f);
    }

    public void OnDebug0(InputAction.CallbackContext context)
    {
        if (context.performed)
            resetMoveSpeed();
    }
    // END DEBUG

    public void setMoveSpeed(float newSpeed)
    {
        currentMoveSpeed = newSpeed;
    }

    public void resetMoveSpeed()
    {
        if (unitData != null)
            currentMoveSpeed = unitData.moveSpeed;
    }

    private void OnDestroy()
    {
        if (playerReference != null && playerReference.playerInstance == gameObject)
            playerReference.playerInstance = null;
    }
}