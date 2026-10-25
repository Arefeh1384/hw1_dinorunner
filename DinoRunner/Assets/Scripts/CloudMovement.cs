
using UnityEngine;

public class CloudMovement : MonoBehaviour
{
    [Header("Cloud Movement")]
    public float moveSpeed = 0.7f;

    [Header("Cloud Reset")]
    public float leftLimit = -11f;
    public float rightResetPosition = 11f;

    void Update()
    {
        // Move the cloud slowly to the left
        transform.Translate(
            Vector3.left * moveSpeed * Time.deltaTime,
            Space.World
        );

        // Move the cloud back to the right
        if (transform.position.x < leftLimit)
        {
            Vector3 newPosition = transform.position;
            newPosition.x = rightResetPosition;
            transform.position = newPosition;
        }
    }
}
