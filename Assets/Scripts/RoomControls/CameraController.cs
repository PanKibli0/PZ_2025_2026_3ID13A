using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Camera Settings")]
    [SerializeField] private float smoothSpeed = 0.6f;
    [SerializeField] private Camera cam;
    [SerializeField] private float baseRoomHeight = 10f;
    [SerializeField] private float zoomSpeed = 5f;

    private float targetOrthoSize;
    private Vector3 targetPosition;
    private Vector3 velocity = Vector3.zero;

    private void OnEnable()
    {
        EventBus.OnRoomEntered += UpdateCameraBounds;
    }

    private void OnDisable()
    {
        EventBus.OnRoomEntered -= UpdateCameraBounds;
    }
    private void Awake()
    {
        if (cam == null)
            cam = GetComponent<Camera>();

        targetOrthoSize = cam.orthographicSize;
    }
    public void Init(Transform playerTransform)
    {
    }

    public void SetRoom(Vector2 roomCenter)
    {
        targetPosition = new Vector3(roomCenter.x, roomCenter.y, -10f);
        transform.position = targetPosition;
    }

    private void UpdateCameraBounds(Vector2 roomCenter, Vector2 roomSize)
    {
        targetPosition = new Vector3(roomCenter.x, roomCenter.y, -10f);

        float scale = roomSize.y / baseRoomHeight;
        targetOrthoSize = 5.6f * scale;
    }

    private void LateUpdate()
    {
        transform.position = Vector3.SmoothDamp(
            transform.position,
            targetPosition,
            ref velocity,
            smoothSpeed);

        cam.orthographicSize = Mathf.Lerp(
            cam.orthographicSize,
            targetOrthoSize,
            zoomSpeed * Time.deltaTime);
    }
}