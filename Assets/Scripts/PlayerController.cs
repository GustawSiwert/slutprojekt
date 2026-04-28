using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    float gravity = 0.06f;
    float swimStrength = 5;
    float waterRes = 0.03f;
    float freeFallSpeed = 3;

    Rigidbody2D rb;

    Vector2 velocity;
    Vector2 towardsMouse;

    bool swimPressed = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector2 playerPos = transform.position;
        towardsMouse = (mousePos - playerPos).normalized;

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            swimPressed = true;

        }

    }
    private void FixedUpdate()
    {
        velocity = rb.linearVelocity;
        swim();
        waterResistance();
        gravitation();
        rb.linearVelocity = velocity;
    }



    void swim()
    {
        if (swimPressed)
        {
            velocity += swimStrength * towardsMouse;
            swimPressed = false;
        }
    }
    void waterResistance()
    {

        if (velocity.x > 0)
        {
            velocity.x -= waterRes;
        }
        else if (velocity.x < 0)
        {
            velocity.x += waterRes;
        }
        if (velocity.y > 0)
        {
            velocity.y -= waterRes;
        }
        else if (velocity.y < 0)
        {
            velocity.y += waterRes;
        }
    }
    void gravitation()
    {
        if (velocity.y > -freeFallSpeed)
        {
            velocity.y -= gravity;
        }

    }
}
