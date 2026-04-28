using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    float gravity = 0.08f;
    float swimStrength = 5;
    float waterRes = 0.05f;
    float freeFallSpeed = 3;
    float maxSpeed = 7f;
    

    Rigidbody2D rb;

    Vector2 velocity;
    Vector2 towardsMouse;

    bool swimPressed = false;
    bool inWater = true;
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
        if (transform.position.y < 11) { inWater = true; }
        else { inWater = false; }

    }
    private void FixedUpdate()
    {
        velocity = rb.linearVelocity;
        swim();
        waterResistance();
        gravitation();
        limitSpeed();
        rb.linearVelocity = velocity;
    }



    void swim()
    {
        if (swimPressed && inWater)
        {
            velocity += swimStrength * towardsMouse;
            swimPressed = false;
        }
        swimPressed = false ;
    }
    void waterResistance()
    {
        if (!inWater) return;
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
        if (!inWater) 
        {
            if (velocity.y > -freeFallSpeed*3)
            {
                velocity.y -= gravity*3.5f;
            }
            if (velocity.y < -freeFallSpeed * 3)
            {
                velocity.y = -freeFallSpeed * 3;
            }
            return; 
        }
        
        if (velocity.y > -freeFallSpeed)
        {
            velocity.y -= gravity;
        }
        if (velocity.y < -freeFallSpeed )
        {
            velocity.y = -freeFallSpeed;
        }

    }
    void limitSpeed()
    {
        if (!inWater) return;

        if (velocity.magnitude > maxSpeed)
        {
            velocity = velocity.normalized * maxSpeed;
        }
    }
}
