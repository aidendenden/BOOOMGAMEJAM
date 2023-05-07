using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInputManage : MonoBehaviour
{
    public float speed = 5f;
    private Vector3 moveInput;
    private Rigidbody playerRig;

    private void Start()
    {
        if (transform.TryGetComponent<Rigidbody>(out playerRig))
        {
            Debug.Log("Found Rigidbody component: " + playerRig.name);
        }
        else
        {
            Debug.Log("Could not find Rigidbody component");
        }
    }

    void Update()
    {
        Vector3 position = transform.position;
        
        position.x += moveInput.x * speed * Time.deltaTime;
        position.y += moveInput.y * speed * Time.deltaTime;

        playerRig.MovePosition(position);
        
        if (moveInput.x > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (moveInput.x < 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
 
}