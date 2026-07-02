using UnityEngine;

public class RoomTransition : MonoBehaviour
{
    [SerializeField] private Transform targetTransitionPoint;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        RoomController currentRoom = GetComponentInParent<RoomController>();

        if (currentRoom != null)
        {
            currentRoom.DespawnEnemies();
        }

        collision.transform.position = targetTransitionPoint.position;
        RoomController nextRoom = targetTransitionPoint.GetComponentInParent<RoomController>();

        if (nextRoom != null)
        {
            EventBus.publishOnRoomEntered(
                nextRoom.RoomCenter.position,
                nextRoom.RoomSize
            );

            nextRoom.PlayerEnteredRoom();
        }
    }
}