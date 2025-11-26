using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    float speed;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (GameConfigLoader.Config != null)
            speed = GameConfigLoader.Config.player_data.speed;
        else
            speed = 3f;
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(h, 0, v).normalized * speed;
        rb.linearVelocity = new Vector3(move.x, rb.linearVelocity.y, move.z);
    }
}
