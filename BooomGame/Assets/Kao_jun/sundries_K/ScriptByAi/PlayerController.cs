using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 10f; // 跳跃力度
    public float moveSpeed = 5f; // 移动速度

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        

        
    }

    public void Jump(InputAction.CallbackContext ctx)
    {
        rb.AddForce(new Vector2(rb.velocity.x, jumpForce));
    }
}

