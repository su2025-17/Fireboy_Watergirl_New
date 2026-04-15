using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 720f;
    public float jumpForce = 5f;
    
    // Choose between "Arrows" or "WASD" in the Inspector
    public enum ControlType { Arrows, WASD }
    public ControlType controlScheme;

    private Rigidbody rb;
    private bool isGrounded;
    private Vector3 moveDir;

    void Start() {
        rb = GetComponent<Rigidbody>();
    }

    void Update() {
        float h = 0;
        float v = 0;

        if (controlScheme == ControlType.Arrows) {
            if (Input.GetKey(KeyCode.UpArrow)) v = -1;   // Move away from camera on Up
            if (Input.GetKey(KeyCode.DownArrow)) v = 1;  // Move toward camera on Down
            if (Input.GetKey(KeyCode.LeftArrow)) h = -1;
            if (Input.GetKey(KeyCode.RightArrow)) h = 1;
            
            // Jump for Arrows (Space)
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded) Jump();
        } 
        else if (controlScheme == ControlType.WASD) {
            if (Input.GetKey(KeyCode.W)) v = 1;
            if (Input.GetKey(KeyCode.S)) v = -1;
            if (Input.GetKey(KeyCode.A)) h = -1;
            if (Input.GetKey(KeyCode.D)) h = 1;
            
            // Jump for WASD (Left Shift or whatever you prefer)
            if (Input.GetKeyDown(KeyCode.LeftShift) && isGrounded) Jump();
        }

        moveDir = new Vector3(h, 0, v).normalized;
    }

    void FixedUpdate() {
        if (moveDir.magnitude >= 0.1f) {
            rb.MovePosition(rb.position + moveDir * moveSpeed * Time.fixedDeltaTime);
            Quaternion targetRot = Quaternion.LookRotation(moveDir, Vector3.up);
            rb.MoveRotation(Quaternion.RotateTowards(rb.rotation, targetRot, rotationSpeed * Time.fixedDeltaTime));
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