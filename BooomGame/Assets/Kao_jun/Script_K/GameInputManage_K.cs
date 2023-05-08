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
    private Animator _animator;
    private float StopX_k, StopY_k;//用于储存输入的方向传给animator中判断方向

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


        if(GameObject.FindGameObjectWithTag("Player").TryGetComponent<Animator>(out _animator))
        {
            Debug.Log("Found Animator component: " + _animator.name);
        }
        else
        {
            Debug.Log("Could not find Animator component");
        }

    }

    void Update()
    {
        Vector3 position = transform.position;
        
        position.x += moveInput.x * speed * Time.deltaTime;
        position.z += moveInput.y * speed * Time.deltaTime;//暂时是在zx平面上移动

        WalkAnimationController_K();//判断player动画

        playerRig.MovePosition(position);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
 

    private void WalkAnimationController_K()//这个是给主角判断动画的
    {
        if (!_animator)
        {
            return;
        }
        if (moveInput != Vector3.zero)
        {
            _animator.SetBool("isRuning", true);
            StopX_k = moveInput.x;
            StopY_k = moveInput.y;//如果在移动就更新用于判断动画状态的xy

        }
        else
        {
            _animator.SetBool("isRuning", false);//不在移动就不更新用于动画判断的xy
        }

        _animator.SetFloat("inputX", StopX_k);
        _animator.SetFloat("inputY", StopY_k);

    }
}