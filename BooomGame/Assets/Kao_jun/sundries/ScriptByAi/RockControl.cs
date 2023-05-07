using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RockControl : MonoBehaviour
{
    public float jumpForce = 10f;
    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
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
            rb.AddForce(new Vector3(rb.velocity.x, jumpForce, rb.velocity.z));
        }
    }
}
