using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 0.001f;
    public float jumpForce = 5f;
    
    // Choose between "Arrows" or "WASD" in the Inspector
    public enum ControlType { Arrows, WASD }
    public ControlType controlScheme;

    private Rigidbody rb;
    private bool isGrounded;
    private Vector3 moveDir;
    private float turnInput;

    void Start() {
        rb = GetComponent<Rigidbody>();
    }

    void Update() {
        float forward = 0f;
        float turn = 0f;

        if (controlScheme == ControlType.Arrows) {
            if (Input.GetKey(KeyCode.UpArrow)) forward = 1;
            if (Input.GetKey(KeyCode.DownArrow)) forward = -1;
            if (Input.GetKey(KeyCode.LeftArrow)) turn = -1;
            if (Input.GetKey(KeyCode.RightArrow)) turn = 1;
            
            if (Input.GetKeyDown(KeyCode.Space)) Jump();
        } 
        else if (controlScheme == ControlType.WASD) {
            if (Input.GetKey(KeyCode.W)) forward = 1;
            if (Input.GetKey(KeyCode.S)) forward = -1;
            if (Input.GetKey(KeyCode.A)) turn = -1;
            if (Input.GetKey(KeyCode.D)) turn = 1;
            
            if (Input.GetKeyDown(KeyCode.LeftShift)) Jump();
        }

        moveDir = transform.forward * forward;
        moveDir.y = 0f;
        moveDir = moveDir.normalized;
        turnInput = turn;
    }

    void FixedUpdate() {
        if (turnInput != 0f) {
            rb.MoveRotation(rb.rotation * Quaternion.Euler(0f, turnInput * rotationSpeed * Time.fixedDeltaTime, 0f));
        }

        if (moveDir.magnitude >= 0.1f) {
            rb.MovePosition(rb.position + moveDir * moveSpeed * Time.fixedDeltaTime);
        }
    }

    void Jump() {
        // Increase the force significantly (try 10 or 12 instead of 5)
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isGrounded = false;
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Ground")) isGrounded = true;
    }
}