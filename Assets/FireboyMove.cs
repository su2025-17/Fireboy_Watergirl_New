using UnityEngine;

public class FireboyMove : MonoBehaviour
{
    private Animator anim;
    public float speed = 5f;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        float input = Input.GetAxis("Horizontal"); // A/D or Left/Right arrows

        // Move the character left/right
        transform.Translate(Vector3.right * input * speed * Time.deltaTime);

        // Tell the Animator to switch clips
        if (input != 0)
        {
            anim.SetBool("isWalking", true);
            
            // Flip the character to face the direction of movement
            float direction = input > 0 ? 1 : -1;
            transform.localScale = new Vector3(direction, 1, 1);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }
    }
}