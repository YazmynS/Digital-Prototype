using UnityEngine;

public class BounceOrStick : MonoBehaviour
{
    //for bounce use the polygon
    public float bounceForce;
    public float bounceGravity;
    public float bounceRotationSpeed;
    public bool isStuck = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //bounce when collision with wall
        if (collision.gameObject.CompareTag("leftWall") || collision.gameObject.CompareTag("rightWall"))
        {
            if (isStuck == false)
            {
                //bounce
                Debug.Log("Bounce");

                Rigidbody2D rb = GetComponent<Rigidbody2D>();

                //Vector2 bounceDirection = Vector2.Reflect(transform.up, collision.contacts[0].normal);
                //rb.linearVelocity = bounceDirection * bounceForce;

                // Determine the collision direction
                if (collision.gameObject.CompareTag("leftWall"))
                {
                    // Hit from the left, bounce right
                    rb.linearVelocity = new Vector2(bounceForce, rb.linearVelocity.y - bounceGravity);
                }
                else if (collision.gameObject.CompareTag("rightWall"))
                {
                    // Hit from the right, bounce left
                    rb.linearVelocity = new Vector2(-bounceForce, rb.linearVelocity.y - bounceGravity);
                }

                // Apply torque to spin the asset
                rb.AddTorque(bounceRotationSpeed, ForceMode2D.Force);

                //instead of bounceGravoty variable
                // Allow Rigidbody2D to handle gravity
                //rb.gravityScale = 1f; // Ensure gravity is applied

            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //stick to wall when box collider is triggered
        Debug.Log("Stick");
        isStuck = true;
        
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        // Get the position of the colliding object
        Vector3 targetPosition = collision.transform.position;

        if(collision.gameObject.tag == "rightWall")
        {
            // if rightwall, Set the new position 1 units to the left
            transform.position = new Vector3(targetPosition.x - 1, targetPosition.y, targetPosition.z);

            //stop movement
            rb.linearVelocity = Vector2.zero;
            transform.rotation = Quaternion.identity; // Reset rotation to its original orientation
            rb.linearVelocity = Vector2.zero;        // Reset linear velocity
            rb.angularVelocity = 0f;                 // Reset angular velocity to prevent spinning
            rb.gravityScale = 0;
        }

        //if leftwall set position 1 unit to the right
        if (collision.gameObject.tag == "leftWall")
        {
            // if rightwall, Set the new position 1 units to the left
            transform.position = new Vector3(targetPosition.x + 1, targetPosition.y, targetPosition.z);

            //stop movement
            rb.linearVelocity = Vector2.zero;
            transform.rotation = Quaternion.identity; // Reset rotation to its original orientation
            rb.linearVelocity = Vector2.zero;        // Reset linear velocity
            rb.angularVelocity = 0f;                 // Reset angular velocity to prevent spinning
            rb.gravityScale = 0;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("exit trigger");
        isStuck = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
