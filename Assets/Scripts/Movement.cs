using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private float powerUp = 0;
    public Sprite newSprite;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump") || Input.GetKey(KeyCode.W))
        {
            Jump();
        }

        if(powerUp == 2)
        {
            GetComponent < SpriteRenderer >().sprite = newSprite;
        }
            

        Vector3 newScale = transform.localScale;
        if (Input.GetAxis("Horizontal") > 0.0f)
            newScale.x = -5.0f;
        else if (Input.GetAxis("Horizontal") < 0.0f)
            newScale.x = 5.0f;

        transform.localScale = newScale;
    }

    private void FixedUpdate()
    {
        if (rb2d)
        {
            rb2d.AddForce(new Vector2(Input.GetAxis("Horizontal") * 12, 0));
            
        }
    }

    private void Jump()
    {
        if (rb2d)
        {
            if (Mathf.Abs(rb2d.velocity.y) < 0.0005f)
            {
                rb2d.AddForce(new Vector2 (0, 7.5f), ForceMode2D.Impulse);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PowerUp"))
        {
            Destroy(collision.gameObject);
            powerUp += 1;
        }
        else if (collision.gameObject.CompareTag("Enemy") && powerUp != 2)
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Enemy") && powerUp == 2)
        {
            Destroy(collision.gameObject);
        }
    }
}
