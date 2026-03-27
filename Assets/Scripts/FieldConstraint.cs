using UnityEngine;

public class FieldConstraint : MonoBehaviour
{
    public Transform playerTransform;//later needs to be manually added when we activate zones
    /// <summary>
    /// left, right, down, up;
    /// </summary>
    public Vector4 boundary = new Vector4(15, 15, 10, 10);
    // Update is called once per frame
    void FixedUpdate()
    {
        if (checkBounadary())
        {
            Debug.Log("CHEETAH!");
            playerTransform.position = transform.position;
        }
    }
    private bool checkBounadary()
    {
        float x = playerTransform.position.x;
        float y = playerTransform.position.y;
        return ((x < transform.position.x - boundary[0] || x > transform.position.x + boundary[1]) ||
            ((y < transform.position.y - boundary[2] || y > transform.position.y + boundary[3])));
    }
}
