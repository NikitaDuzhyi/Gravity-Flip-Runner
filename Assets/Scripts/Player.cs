using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;

    private bool upsideDown = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            FlipGravity();
        }
    }

    private void FlipGravity()
    {
        upsideDown = !upsideDown;

        rb.gravityScale *= -1;

        transform.Rotate(0, 180, 180);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
            GameManager.Instance.GameOver();
        }
    }
}