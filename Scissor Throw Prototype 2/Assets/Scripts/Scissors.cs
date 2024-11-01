using UnityEngine;
using UnityEngine.SceneManagement;

public class Scissors : MonoBehaviour
{
    public Vector3 Initial_Pos;

    [SerializeField]
    private int Scissor_Speed;
    [SerializeField]
    private string Scene_Name;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Camera mainCamera;
    private Vector3 dragPosition; // Stores the position where the mouse was released

    public void Awake()
    {
        Initial_Pos = transform.position;
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        mainCamera = Camera.main;

        if (rb == null)
        {
            Debug.LogError("Rigidbody2D component is missing.");
        }

        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer component is missing.");
        }

        if (mainCamera == null)
        {
            Debug.LogError("Main camera is not found.");
        }
    }

    public void OnMouseDown()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.yellow;
        }
    }

    public void OnMouseDrag()
    {
        // Update dragPosition with the mouse's position in world coordinates, without moving the scissors
        dragPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        dragPosition.z = 0; // Ensure dragPosition is on the 2D plane
    }

    public void OnMouseUp()
    {
        if (rb != null)
        {
            // Calculate spring force based on the difference between the initial position and the drag end position
            Vector2 springForce = (transform.position - dragPosition) * Scissor_Speed;

            if (spriteRenderer != null)
            {
                spriteRenderer.color = Color.red;
            }

            rb.gravityScale = 1;
            rb.AddForce(springForce); // Apply the calculated force to the Rigidbody
        }
    }

    void Update()
    {
        if (IsOutOfCameraBounds())
        {
            ResetScissors();
        }
    }

    private bool IsOutOfCameraBounds()
    {
        if (mainCamera == null) return false;

        // Get the camera boundaries
        float cameraHeight = 2f * mainCamera.orthographicSize;
        float cameraWidth = cameraHeight * mainCamera.aspect;

        // Calculate boundaries
        float leftBound = mainCamera.transform.position.x - cameraWidth;
        float rightBound = mainCamera.transform.position.x + cameraWidth;
        float topBound = mainCamera.transform.position.y + cameraHeight;
        float bottomBound = mainCamera.transform.position.y - cameraHeight / 3;

        // Check if scissors are out of bounds
        return transform.position.x < leftBound || transform.position.x > rightBound ||
               transform.position.y < bottomBound || transform.position.y > topBound;
    }

    private void ResetScissors()
    {
        // Reset position, rotation, linearVelocity, gravity, and color
        transform.position = Initial_Pos;
        transform.rotation = Quaternion.identity; // Reset rotation to its original orientation
        rb.linearVelocity = Vector2.zero;        // Reset linear velocity
        rb.angularVelocity = 0f;                 // Reset angular velocity to prevent spinning
        rb.gravityScale = 0;

        if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.white;
        }
    }
}
