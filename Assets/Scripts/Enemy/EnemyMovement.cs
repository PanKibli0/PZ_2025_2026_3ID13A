using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private UnitDataSO unitData;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private PlayerReferenceSO playerReference;

    private void Start()
    {
        if (unitData != null)
            rb.linearDamping = unitData.linearDamping;
    }

    private void FixedUpdate()
    {
        if (playerReference == null || playerReference.playerInstance == null || unitData == null) return;

        Vector2 direction = (playerReference.playerInstance.transform.position - transform.position).normalized;
        rb.linearVelocity = direction * unitData.moveSpeed;
    }
}