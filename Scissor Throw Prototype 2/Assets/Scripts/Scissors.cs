using UnityEngine;
using UnityEngine.SceneManagement;

public class Scissors : MonoBehaviour
{
    public Vector3 Initial_Pos;
    public int Scissor_Speed;
    public string Scene_Name;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Camera mainCamera;

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

    public void OnMouseUp()
    {
        if (rb != null)
        {
            Vector2 springForce = (Initial_Pos - transform.position) * Scissor_Speed;
            if (spriteRenderer != null)
            {
                spriteRenderer.color = Color.red;
            }
            rb.gravityScale = 1;
            rb.AddForce(springForce);
        }
    }

    public void OnMouseDrag()
    {
        Vector3 dragPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        dragPosition.z = 0; // Ensure the object stays on the 2D plane
        transform.position = dragPosition;
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
        float leftBound = mainCamera.transform.position.x - cameraWidth / 2;
        float rightBound = mainCamera.transform.position.x + cameraWidth / 2;
        float topBound = mainCamera.transform.position.y + cameraHeight / 2;
        float bottomBound = mainCamera.transform.position.y - cameraHeight / 2;

        // Check if scissors are out of bounds
        return transform.position.x < leftBound || transform.position.x > rightBound ||
               transform.position.y < bottomBound || transform.position.y > topBound;
    }

    private void ResetScissors()
    {
        // Reset position, velocity, gravity, and color
        transform.position = Initial_Pos;
        rb.velocity = Vector2.zero;
        rb.gravityScale = 0;

        if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.white;
        }
    }
}
