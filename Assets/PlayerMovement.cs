using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
<<<<<<< Updated upstream
    public float rotationSpeed = 0.001f;
    public float jumpForce = 5f;
=======
    public float rotationSpeed = 100f; // Increased from 0.001 for better turning
    public float jumpForce = 8f;
>>>>>>> Stashed changes
    
    public enum ControlType { Arrows, WASD }
    public ControlType controlScheme;
    public AudioClip jumpSoundArrows;
    public AudioClip jumpSoundWASD;

    private Rigidbody rb;
    private Animator anim; // Added Animator reference
    private bool isGrounded;
    private Vector3 moveDir;
    private float turnInput;
<<<<<<< Updated upstream
=======
    private AudioSource audioSource;
>>>>>>> Stashed changes

    void Start() {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>(); // Connect to the Animator component
        
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null) {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
        }
    }

    void Update() {
        float forward = 0f;
        float turn = 0f;

        if (controlScheme == ControlType.Arrows) {
            if (Input.GetKey(KeyCode.UpArrow)) forward = 1;
            if (Input.GetKey(KeyCode.DownArrow)) forward = -1;
            if (Input.GetKey(KeyCode.LeftArrow)) turn = -1;
            if (Input.GetKey(KeyCode.RightArrow)) turn = 1;
<<<<<<< Updated upstream
            
=======
>>>>>>> Stashed changes
            if (Input.GetKeyDown(KeyCode.Space)) Jump();
        } 
        else if (controlScheme == ControlType.WASD) {
            if (Input.GetKey(KeyCode.W)) forward = 1;
            if (Input.GetKey(KeyCode.S)) forward = -1;
            if (Input.GetKey(KeyCode.A)) turn = -1;
            if (Input.GetKey(KeyCode.D)) turn = 1;
<<<<<<< Updated upstream
            
            if (Input.GetKeyDown(KeyCode.LeftShift)) Jump();
        }

=======
            if (Input.GetKeyDown(KeyCode.LeftShift)) Jump();
        }

        // --- ANIMATION LOGIC ---
        // If we are moving forward or backward, tell the animator to walk
        if (anim != null) {
            bool isMoving = (forward != 0);
            anim.SetBool("isWalking", isMoving);
        }

>>>>>>> Stashed changes
        moveDir = transform.forward * forward;
        moveDir.y = 0f;
        moveDir = moveDir.normalized;
        turnInput = turn;
    }

    void FixedUpdate() {
        if (turnInput != 0f) {
<<<<<<< Updated upstream
            rb.MoveRotation(rb.rotation * Quaternion.Euler(0f, turnInput * rotationSpeed * Time.fixedDeltaTime, 0f));
=======
            // Apply rotation
            Quaternion turnRotation = Quaternion.Euler(0f, turnInput * rotationSpeed * Time.fixedDeltaTime, 0f);
            rb.MoveRotation(rb.rotation * turnRotation);
>>>>>>> Stashed changes
        }

        if (moveDir.magnitude >= 0.1f) {
            rb.MovePosition(rb.position + moveDir * moveSpeed * Time.fixedDeltaTime);
        }
    }

    void Jump() {
        if (isGrounded) { // Only jump if on the ground
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
            
            AudioClip jumpSound = (controlScheme == ControlType.Arrows) ? jumpSoundArrows : jumpSoundWASD;
            if (jumpSound != null) {
                audioSource.PlayOneShot(jumpSound);
            }
        }
    }

    void OnCollisionEnter(Collision collision) {
        // Make sure your floor/ground object has the Tag "Ground" in the Inspector!
        if (collision.gameObject.CompareTag("Ground")) {
            isGrounded = true;
        }
    }
}