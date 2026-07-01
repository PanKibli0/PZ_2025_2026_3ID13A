using UnityEngine;

public class RoomTransition : MonoBehaviour
{
    [SerializeField] private Transform targetTransitionPoint;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        collision.transform.position = targetTransitionPoint.position;

        RoomController room = targetTransitionPoint.GetComponentInParent<RoomController>();

        if (room != null)
        {
            EventBus.publishOnRoomEntered(
                room.RoomCenter.position,
                room.RoomSize
            );
        }
    }
}