using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RockControl : MonoBehaviour
{
    public float jumpForce = 10f;
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }




    public void Test_SayHI(InputAction.CallbackContext ctx)
    {
        
        if(ctx.phase == InputActionPhase.Performed)
        {
            Debug.Log("Hi!");
        }
    }
    public void Jump(InputAction.CallbackContext ctx)
    {
       
        if (ctx.phase == InputActionPhase.Performed)
        {
            rb.AddForce(new Vector2(rb.velocity.x, jumpForce));
        }
    }
}
