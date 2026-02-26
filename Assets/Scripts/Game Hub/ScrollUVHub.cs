using UnityEngine;

public class ScrollUVHub : MonoBehaviour
{
    public Transform player;       // Player to track
    public float xParallax = 2f;    // Parallax factor
    public float yParallax = 2f;

    private MeshRenderer mr;
    private Material mat;
    private Vector2 startOffset;

    void Start()
    {
        // Cache material
        mr = GetComponent<MeshRenderer>();
        mat = mr.material;
        startOffset = mat.mainTextureOffset;
    }

    void LateUpdate()
    {
        // Lock rotation: keep X=90°, Y=0°, Z=0°
        transform.rotation = Quaternion.Euler(90f, 0f, 0f);

        // Compute new offsets based on player's position
        Vector2 offset = startOffset;
        offset.x += player.position.x / transform.localScale.x / xParallax * 10f;
        offset.y += player.position.z / transform.localScale.z / yParallax;

        mat.mainTextureOffset = offset;
    }
}
