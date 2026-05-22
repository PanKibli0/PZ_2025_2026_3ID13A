using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] private float speed = 5f;

    private int direction = 1;

    void Update()
    {
        transform.Translate(Vector2.right * direction * speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        direction *= -1;
    }
}