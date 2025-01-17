using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Camera mainCamera;
    public float speed = 5f;
    public float jumpForce = 10f;
    private Rigidbody2D rb;
    private bool isGrounded;
    private Vector3 screenBounds;
    private float objectWidth;
    private float objectHeight;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        Vector3 bottomLeft = mainCamera.ScreenToWorldPoint(new Vector3(0, 0, mainCamera.transform.position.z));
        Vector3 topRight = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
        screenBounds = new Vector2(topRight.x, topRight.y);

        objectWidth = transform.GetComponent<SpriteRenderer>().bounds.size.x / 2;
        objectHeight = transform.GetComponent<SpriteRenderer>().bounds.size.y / 2;

    }

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveX * speed, rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            isGrounded = false;
        }

        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, -screenBounds.x + objectWidth, screenBounds.x - objectWidth);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, -screenBounds.y + objectHeight, screenBounds.y - objectHeight);
        transform.position = clampedPosition;

    }

}
