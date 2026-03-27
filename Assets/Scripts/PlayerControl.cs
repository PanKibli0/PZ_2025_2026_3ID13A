using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    public Vector2 moveInput;
    public float attemptedLinearVelocity;
    public float effectiveLinearVelocity;

    [Header("FFS make sure it's connected")]
    [SerializeField]
    PlayerDynamicDataSO dataSO;
    [Header("Don't mind those, initialized at the start")]
    [SerializeField]
    PlayerInput playerInput;
    [SerializeField]
    Rigidbody2D rigidbody2d;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        rigidbody2d.linearDamping = dataSO.linearVelocityDamping;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        velocityChange();
    }

    /// <summary>
    /// Event handler for 'Move' action.
    /// </summary>
    /// <param name="context"></param>
    public void OnMove(InputAction.CallbackContext context)
    {
        // read the value for the "move" action each event call
        moveInput = context.ReadValue<Vector2>();
    }

    private void velocityChange()
    {
        Vector2 inputVel = moveInput * dataSO.movementSpeedMultiplier;
        Vector2 newVel = rigidbody2d.linearVelocity + inputVel / rigidbody2d.mass * Time.fixedDeltaTime;
        attemptedLinearVelocity = newVel.magnitude;
        if (attemptedLinearVelocity > dataSO.maxMovementSpeed)
        {
            rigidbody2d.linearVelocity = newVel.normalized * dataSO.maxMovementSpeed;
        }
        else
        {
            rigidbody2d.AddForce(inputVel, ForceMode2D.Force);
        }
        effectiveLinearVelocity = rigidbody2d.linearVelocity.magnitude;
    }

    public void takeDmg(int dmg)
    {
        dataSO.hp -= dmg;
    }
}
