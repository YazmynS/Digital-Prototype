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
        // Bounce when collision with wall
        if (collision.gameObject.CompareTag("Wall"))
        {
            if (!isStuck)
            {
                Debug.Log("Bounce");
                Rigidbody2D rb = GetComponent<Rigidbody2D>();

                // Use reflection for a natural bounce
                Vector2 bounceDirection = Vector2.Reflect(rb.linearVelocity, collision.contacts[0].normal);
                rb.linearVelocity = new Vector2(bounceDirection.x * bounceForce, rb.linearVelocity.y - bounceGravity);

                // Apply torque to spin the asset
                rb.AddTorque(bounceRotationSpeed, ForceMode2D.Force);
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //stick to wall when box collider is triggered
        Debug.Log("Stick");

        isStuck = true;
        
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        if(collision.gameObject.tag == "Wall")
        {

            //stop movement
            rb.linearVelocity = Vector2.zero;
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
