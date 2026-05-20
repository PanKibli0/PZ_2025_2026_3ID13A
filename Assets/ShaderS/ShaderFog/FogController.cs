using UnityEngine;

public class FogController : MonoBehaviour
{
    [SerializeField] private Material fogMaterial;
    [SerializeField] private Camera cam;

    private Transform player;

    void Start()
    {
        if (cam == null)
            cam = Camera.main;

        findPlayer();
    }

    void Update()
    {
        if (player == null)
        {
            findPlayer();
            return;
        }

        Vector3 screenPos = cam.WorldToViewportPoint(player.position);

        fogMaterial.SetVector(
            "_PlayerPos",
            new Vector4(screenPos.x, screenPos.y, 0, 0)
        );
    }

    void findPlayer()
    {
        GameObject obj = GameObject.FindGameObjectWithTag("Player");

        if (obj != null)
            player = obj.transform;
    }
}