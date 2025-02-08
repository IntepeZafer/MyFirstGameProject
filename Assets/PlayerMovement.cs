using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    private bool isGrounded = true;
    private bool canDoubleJump;
    public Transform groundCheck;
    public LayerMask groundLayer;
    private Animator animator;
    public float walkSpeed = 5f;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        animator.SetFloat("Walk", Mathf.Abs(moveX));
        if(moveX > 0)
        {
            transform.localScale = new Vector3(5f, 5f, 1f);
        }else if(moveX < 0)
        {
            transform.localScale = new Vector3(-5f, 5f, 1f);
        }
        transform.Translate(moveX * moveSpeed * Time.deltaTime, 0f, 0f);
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
        if (isGrounded)
        {
            canDoubleJump = true;
        }
        if(isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }else if(canDoubleJump && Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            canDoubleJump = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            moveSpeed = 1f;
        }
    }
}
