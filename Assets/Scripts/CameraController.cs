using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public float moveSpeed = 5f;

    private bool isMoving = false;

    public bool IsMoving()
    {
        return isMoving;
    }

    public void MoveByOffset(Vector3 offset)
    {
        if (!isMoving)
        {
            Vector3 targetPosition = transform.position + offset;
            StartCoroutine(MoveCamera(targetPosition));
        }
    }

    IEnumerator MoveCamera(Vector3 targetPosition)
    {
        isMoving = true;

        Vector3 startPos = transform.position;

        float time = 0f;

        while (time < 1f)
        {
            time += Time.deltaTime * moveSpeed;
            transform.position = Vector3.Lerp(startPos, targetPosition, time);
            yield return null;
        }

        isMoving = false;
    }
}