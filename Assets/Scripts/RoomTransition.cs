using UnityEngine;

public class RoomTransition : MonoBehaviour
{
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }

    public Direction direction;

    [Header("Player teleport")]
    public float playerOffset = 5f;

    [Header("Camera movement")]
    public float horizontalRoomSize = 30f;
    public float verticalRoomSize = 20f;

    private bool isUsed = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isUsed) return;

        if (other.CompareTag("Player"))
        {
            CameraController cam = Camera.main.GetComponent<CameraController>();

            if (cam.IsMoving()) return;

            isUsed = true;

            Vector3 playerMove = GetPlayerOffset();
            Vector3 cameraMove = GetCameraOffset();

            other.transform.position += playerMove;

            cam.MoveByOffset(cameraMove);

            Invoke(nameof(ResetUsage), 0.5f);
        }
    }

    Vector3 GetPlayerOffset()
    {
        switch (direction)
        {
            case Direction.Up:
                return new Vector3(0, playerOffset, 0);

            case Direction.Down:
                return new Vector3(0, -playerOffset, 0);

            case Direction.Left:
                return new Vector3(-playerOffset, 0, 0);

            case Direction.Right:
                return new Vector3(playerOffset, 0, 0);

            default:
                return Vector3.zero;
        }
    }

    Vector3 GetCameraOffset()
    {
        switch (direction)
        {
            case Direction.Up:
                return new Vector3(0, verticalRoomSize, 0);

            case Direction.Down:
                return new Vector3(0, -verticalRoomSize, 0);

            case Direction.Left:
                return new Vector3(-horizontalRoomSize, 0, 0);

            case Direction.Right:
                return new Vector3(horizontalRoomSize, 0, 0);

            default:
                return Vector3.zero;
        }
    }

    void ResetUsage()
    {
        isUsed = false;
    }
}