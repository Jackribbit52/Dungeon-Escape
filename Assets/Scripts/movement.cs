using UnityEngine;

public class movement : MonoBehaviour
{

    float speedX;
    float speedY;
    public float speed;
    Rigidbody2D rb;
    private bool isFacingRight = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        speedX = Input.GetAxisRaw("Horizontal") * speed;
        speedY = Input.GetAxisRaw("Vertical") * speed;

        rb.linearVelocity = new Vector2(speedX, speedY);
        Flip();
    }

    private void Flip()
    {
        if (isFacingRight && speedX > 0f || !isFacingRight && speedX < 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("goodChest"))
        {
            speed = speed * 2;
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("badChest"))
        {
            speed = speed / 2;
            Destroy(collision.gameObject);
        }
    }
}
