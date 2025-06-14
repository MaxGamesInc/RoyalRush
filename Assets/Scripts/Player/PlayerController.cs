using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{

    Vector2 movement;
    Rigidbody rb;
    [Header("Movement Settings")]
    [Tooltip("Speed at which the player moves")]
    [SerializeField] float moveSpeed = 5f;
    [Tooltip("Maximum movement in the X direction")]
    [SerializeField] float maxMovementX = 5f;
    [Tooltip("Maximum movement in the Y direction")]
    [SerializeField] float maxMovementY = 5f;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
       MovementHandle();


    }
    public void OnMove(InputValue value)
    {
        movement = value.Get<Vector2>();
    }
    

    void MovementHandle()
    {
        Vector3 targetPosition = rb.position + new Vector3(movement.x, 0, movement.y) * (moveSpeed * Time.fixedDeltaTime);
        targetPosition.x =Mathf.Clamp(targetPosition.x, -maxMovementX, maxMovementX);
        targetPosition.z= Mathf.Clamp(targetPosition.z, -maxMovementY, maxMovementY);
        rb.MovePosition(targetPosition);
    }
}
