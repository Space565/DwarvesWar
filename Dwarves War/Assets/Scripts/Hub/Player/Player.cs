using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Joystick joystick;
    [SerializeField] float _movingSpeed;
    Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        PlayerMove();
    }
    private void PlayerMove()
    {
        Vector2 inputVector = joystick.Direction;
        inputVector = inputVector.normalized;
        rb.MovePosition(rb.position + inputVector * (_movingSpeed * Time.fixedDeltaTime));
    }
}
