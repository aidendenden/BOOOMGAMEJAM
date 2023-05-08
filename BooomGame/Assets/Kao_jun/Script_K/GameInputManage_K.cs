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
    private float StopX_k, StopY_k;//���ڴ�������ķ��򴫸�animator���жϷ���

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
        position.z += moveInput.y * speed * Time.deltaTime;//��ʱ����zxƽ�����ƶ�

        WalkAnimationController_K();//�ж�player����

        playerRig.MovePosition(position);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
 

    private void WalkAnimationController_K()//����Ǹ������ж϶�����
    {
        if (!_animator)
        {
            return;
        }
        if (moveInput != Vector3.zero)
        {
            _animator.SetBool("isRuning", true);
            StopX_k = moveInput.x;
            StopY_k = moveInput.y;//������ƶ��͸��������ж϶���״̬��xy

        }
        else
        {
            _animator.SetBool("isRuning", false);//�����ƶ��Ͳ��������ڶ����жϵ�xy
        }

        _animator.SetFloat("inputX", StopX_k);
        _animator.SetFloat("inputY", StopY_k);

    }
}