using UnityEngine;

public class RoomTransition : MonoBehaviour
{
    [SerializeField] private Transform targetTransitionPoint;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.position = targetTransitionPoint.position;
        }
    }
}
