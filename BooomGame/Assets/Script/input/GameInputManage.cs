using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInputManage : MonoBehaviour
{
    public float speed = 5f;
    private Vector2 moveInput;

    void Update()
    {
        Vector3 position = transform.position;
        position.x -= moveInput.x * speed * Time.deltaTime;
        position.y -= moveInput.y * speed * Time.deltaTime;
        transform.position = position;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
}