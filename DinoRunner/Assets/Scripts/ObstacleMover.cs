
using UnityEngine;

public class ObstacleMover : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 4f;
    public float destroyX = -12f;

    [Header("Difficulty Settings")]
    public float speedIncrease = 1f;
    public float increaseInterval = 20f;

    private float currentSpeed;

    void Start()
    {
        currentSpeed = moveSpeed;
    }

    void Update()
    {
        float difficultyLevel =
            Mathf.Floor(Time.timeSinceLevelLoad / increaseInterval);

        currentSpeed = moveSpeed +
            (difficultyLevel * speedIncrease);

        transform.Translate(
            Vector3.left * currentSpeed * Time.deltaTime,
            Space.World
        );

        if (transform.position.x < destroyX)
        {
            Destroy(gameObject);
        }
    }
}
