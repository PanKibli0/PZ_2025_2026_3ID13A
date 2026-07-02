using UnityEngine;

public class PlayerDirectionalAnimation : MonoBehaviour
{
    [Header("Komponenty Bazowe")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Obiekty Kierunków (Pod-prefaby)")]
    [SerializeField] private GameObject prefabPrzod;
    [SerializeField] private GameObject prefabTyl;
    [SerializeField] private GameObject prefabBok;

    [Header("Animatory")]
    [SerializeField] private Animator animPrzod;
    [SerializeField] private Animator animTyl;
    [SerializeField] private Animator animBok;

    [Header("Ustawienia")]
    [SerializeField] private float walkSpeedThreshold = 0.1f;

    [SerializeField] private bool bokDefaultFacesLeft = false;

    private enum Direction { Przod, Tyl, Bok }
    private Direction currentDir = Direction.Przod;

    private void Start()
    {
        SetActiveDirection(Direction.Przod);
    }

    private void Update()
    {
        Vector2 velocity = rb.linearVelocity;
        float speed = velocity.magnitude;

        if (speed > walkSpeedThreshold)
        {
            UpdateDirection(velocity);
        }

        UpdateActiveAnimatorSpeed(speed);
    }

    private void UpdateDirection(Vector2 velocity)
    {
        if (Mathf.Abs(velocity.x) > Mathf.Abs(velocity.y))
        {
            SetActiveDirection(Direction.Bok);

            float scaleX = velocity.x < 0 ? (bokDefaultFacesLeft ? 1f : -1f) : (bokDefaultFacesLeft ? -1f : 1f);
            prefabBok.transform.localScale = new Vector3(scaleX, 1f, 1f);
        }
        else
        {
            if (velocity.y > 0)
                SetActiveDirection(Direction.Tyl);
            else
                SetActiveDirection(Direction.Przod);
        }
    }

    private void SetActiveDirection(Direction newDir)
    {
        currentDir = newDir;

        if (prefabPrzod != null) prefabPrzod.SetActive(currentDir == Direction.Przod);
        if (prefabTyl != null) prefabTyl.SetActive(currentDir == Direction.Tyl);
        if (prefabBok != null) prefabBok.SetActive(currentDir == Direction.Bok);
    }

    private void UpdateActiveAnimatorSpeed(float speed)
    {
        if (currentDir == Direction.Przod && animPrzod != null) animPrzod.SetFloat("Speed", speed);
        if (currentDir == Direction.Tyl && animTyl != null) animTyl.SetFloat("Speed", speed);
        if (currentDir == Direction.Bok && animBok != null) animBok.SetFloat("Speed", speed);
    }
}