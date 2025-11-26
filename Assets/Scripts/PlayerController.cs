using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    float speedMultiplier;
    Vector2 moveInput;

    Transform lastPulpit;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (GameConfigLoader.Config != null)
            speedMultiplier = GameConfigLoader.Config.player_data.speed;
        else
            speedMultiplier = 3f;
    }

    void Update()
    {
        // New Input System - reads WASD and Arrow keys
        moveInput = Vector2.zero;
        
        if (Keyboard.current != null)
        {
            if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed)
                moveInput.y += 1f;
            if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed)
                moveInput.y -= 1f;
            if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
                moveInput.x -= 1f;
            if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
                moveInput.x += 1f;
        }
    }

    void FixedUpdate()
    {
        // Calculate movement direction
        Vector3 movement = new Vector3(moveInput.x, 0, moveInput.y).normalized * speedMultiplier * 1.5f;
        
        // MovePosition - physics-aware, smooth, respects collisions
        Vector3 newPosition = rb.position + movement * Time.fixedDeltaTime;
        rb.MovePosition(newPosition);
    }
}
