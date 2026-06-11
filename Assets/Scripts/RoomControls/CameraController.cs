using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform target;

    [Header("Room Settings")]
    [SerializeField] private float roomWidth = 18f;
    [SerializeField] private float roomHeight = 10f;

    [Header("Camera Settings")]
    [SerializeField] private float smoothSpeed = 0.6f;
    [SerializeField] private Vector3 offset = new Vector3(0, 0, -10f);

    private Vector3 velocity = Vector3.zero;

    public void Init(Transform playerTransform)
    {
        target = playerTransform;
    }

    private void LateUpdate()
    {
        if (target == null) return;
        float targetX = Mathf.Round(target.position.x / roomWidth) * roomWidth;
        float targety = Mathf.Round(target.position.y / roomHeight) * roomHeight;

        Vector3 desiredPosition = new Vector3(targetX, targety, 0) + offset;

        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);
    }
}
