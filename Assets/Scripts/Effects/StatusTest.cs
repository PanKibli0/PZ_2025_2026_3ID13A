using UnityEngine;

public class StatusTest : MonoBehaviour
{
    [SerializeField] private PlayerStatusController statusController;
    [SerializeField] private BlurController blurController;
    [SerializeField] private Health health;
    [SerializeField] private PlayerMovement movement;

    private PlayerWeaponHandler weaponHandler;

    private void Awake()
    {
        weaponHandler = GetComponentInChildren<PlayerWeaponHandler>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            statusController.AddEffect(
                new PoisonStatusEffect(health)
            );

            Debug.Log("Poison applied");
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            statusController.AddEffect(
                new BurnStatusEffect(
                    health,
                    5f,
                    1f,
                    2
                )
            );

            Debug.Log("Burn applied");
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            statusController.AddEffect(
                new TiedStatusEffect(
                    movement,
                    3f
                )
            );
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            statusController.AddEffect(
                new FrozenStatusEffect(
                    movement,
                    5f,
                    0.5f
                )
            );
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            statusController.AddEffect(
                new SlipStatusEffect(
                    movement,
                    10f
                )
            );
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            statusController.AddEffect(
                new LostGlassesStatusEffect(
                    blurController,
                    10f
                )
            );
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            statusController.AddEffect(
                new BubbleStatusEffect(
                    movement,
                    weaponHandler,
                    3f
                )
            );
        }
    }
}