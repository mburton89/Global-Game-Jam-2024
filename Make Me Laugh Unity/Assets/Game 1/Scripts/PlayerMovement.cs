using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    // thanks for the free code, dude! http://www.grizzlypunchgames.com/2d-platformer/how-to-make-2d-platformer-movement-script/

    Rigidbody rb;

    public float moveSpeed;
    public float jumpForce;

    public int jumpsAmount;
    int jumpsLeft;
    public Transform groundCheck;
    public LayerMask groundLayer;

    public bool isGrounded;

    float moveInput;
    float scaleX;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        scaleX = transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.W))
        {
            Jump();
        }

        CheckIfGrounded();
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        Flip();
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
    }

    public void Flip()
    {
        if (moveInput > 0)
        {
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, -180, transform.localEulerAngles.z);
        }
        if (moveInput < 0)
        {
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, 0, transform.localEulerAngles.z);
        }
    }

    public void Jump()
    {
        if (jumpsLeft > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpsLeft--;
        }
    }

    public void CheckIfGrounded()
    {
        isGrounded = Physics.BoxCast(groundCheck.position, groundCheck.GetComponent<BoxCollider>().size / 2, Vector3.down);
        ResetJumps();
    }

    public void ResetJumps()
    {
        if (isGrounded)
        {
            jumpsLeft = jumpsAmount;
        }
    }

    // debug function: use in scene view!
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(groundCheck.position, groundCheck.GetComponent<BoxCollider>().size);
        Gizmos.DrawRay(groundCheck.position, Vector3.down);
    }
}
