using UnityEngine;

public class StatusTest : MonoBehaviour
{
    [SerializeField] private PlayerStatusController statusController;
    [SerializeField] private Health health;

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
    }
}