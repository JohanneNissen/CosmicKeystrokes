using UnityEngine;

public class CubeRotater : MonoBehaviour
{
    [Header("Rotation Settings")]
    [Tooltip("Base rotation speed in degrees per second")]
    public float rotationSpeed = 80f;

    [Tooltip("How often the rotation direction changes (in seconds)")]
    public float changeInterval = 3f;

    [Tooltip("Maximum angle change per direction update")]
    public float maxAngleChange = 180f;

    private Vector3 currentRotationAxis;
    private float timer;

    void Start()
    {
        // Start with a random rotation axis
        PickNewRandomDirection();
    }

    void Update()
    {
        // Rotate the cube every frame
        transform.Rotate(currentRotationAxis * rotationSpeed * Time.deltaTime);

        // Timer to change direction
        timer += Time.deltaTime;

        if (timer >= changeInterval)
        {
            PickNewRandomDirection();
            timer = 0f;
        }
    }

    void PickNewRandomDirection()
    {
        // Generate a completely random axis
        currentRotationAxis = new Vector3(
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f)
        ).normalized;   // Make sure the axis has length 1

        // Optional: Sometimes add some bias toward X, Y, or Z
        // currentRotationAxis = Random.onUnitSphere;
    }
}
