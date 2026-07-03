using UnityEngine;

public class BossDoor : MonoBehaviour
{
    [SerializeField] private GameObject blockVisual;
    [SerializeField] private Transform targetTransitionPoint;

    private bool locked = true;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (locked)
        {
            PlayerInventory inv = other.GetComponentInChildren<PlayerInventory>();

            if (inv != null && inv.HasKey)
            {
                Unlock();
                TransitionRoom(other);
            }
        }
        else
        {
            TransitionRoom(other);
        }
    }

    private void Unlock()
    {
        locked = false;

        if (blockVisual != null)
            blockVisual.SetActive(false);
    }

    private void TransitionRoom(Collider2D collision)
    {
        if (targetTransitionPoint == null)
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