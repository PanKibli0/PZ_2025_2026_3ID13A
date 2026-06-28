using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform target;

    [Header("Room Settings")]
    [SerializeField] private Vector2 cameraViewSize = new Vector2(18f, 10f);

    [Header("Camera Settings")]
    [SerializeField] private float smoothSpeed = 0.6f;
    [SerializeField] private Vector3 offset = new Vector3(0, 0, -10f);

    private Vector2 minBounds;
    private Vector2 maxBounds;
    private Vector3 velocity = Vector3.zero;

    private void OnEnable()
    {
        EventBus.OnRoomEntered += UpdateCameraBounds;
    }

    private void OnDisable()
    {
        EventBus.OnRoomEntered -= UpdateCameraBounds;
    }


    public void Init(Transform playerTransform)
    {
        target = playerTransform;
        UpdateCameraBounds(Vector2.zero, cameraViewSize);
    }

    private void UpdateCameraBounds(Vector2 roomCenter, Vector2 roomSize)
    {
        float halfRoomWidth = roomSize.x / 2f;
        float halfRoomHeight = roomSize.y / 2f;
        float halfCamWidth = cameraViewSize.x / 2f;
        float halfCamHeight = cameraViewSize.y / 2f;

        minBounds = new Vector2(roomCenter.x - halfRoomWidth + halfCamWidth, roomCenter.y - halfRoomHeight + halfCamHeight);
        maxBounds = new Vector2(roomCenter.x + halfRoomWidth - halfCamWidth, roomCenter.y + halfRoomHeight - halfCamHeight);
    }

    private void LateUpdate()
    {
        if (target == null) return;
        float targetX = target.position.x;
        float targety = target.position.y;

        float clampedX = Mathf.Clamp(targetX, minBounds.x, maxBounds.x);
        float clampedY = Mathf.Clamp(targety, minBounds.y, maxBounds.y);

        Vector3 desiredPosition = new Vector3(clampedX, clampedY, 0) + offset;

        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);
    }
}
