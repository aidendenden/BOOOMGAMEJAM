using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInputManage_K : MonoBehaviour
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
        position.z += moveInput.y * speed * Time.deltaTime;//暂时是在zx平面上移动

        playerRig.MovePosition(position);
        
  
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
 
}