using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = System.Random;

public class GameInputManage : MonoBehaviour
{
    private static readonly Lazy<GameInputManage> Lazy = new Lazy<GameInputManage>(() => new GameInputManage());

    private GameInputManage()
    {
    }

    public static GameInputManage Instance => Lazy.Value;

    [HideInInspector] public Vector3 playerLocation;
    public float speed = 5f;
    public float stopDrag = 50f;//用于在物理运动中实现缓停
    public List<AudioClip> FootAudioClips;

    private Vector3 _moveInput;
    private Rigidbody _playerRig;
    private Animator _animator;
    private AudioSource _audioSource;
   
    private float _stopXk, _stopYk; //用于储存输入的方向传给animator中判断方向
    private static readonly int IsRunning = Animator.StringToHash("isRunning");
    private static readonly int InputX = Animator.StringToHash("inputX");
    private static readonly int InputY = Animator.StringToHash("inputY");

    private void Start()
    {
        if (transform.TryGetComponent<Rigidbody>(out _playerRig))
        {
            Debug.Log("Found Rigidbody component: " + _playerRig.name);
        }
        else
        {
            Debug.Log("Could not find Rigidbody component");
        }


        if (transform.TryGetComponent<AudioSource>(out _audioSource))
        {
            _audioSource.enabled = true;
            Debug.Log("Found AudioSource component: " + _audioSource.name);
        }
        else
        {
            Debug.Log("Could not find AudioSource component");
        }

        if (GameObject.FindGameObjectWithTag("Player").TryGetComponent<Animator>(out _animator))
        {
            Debug.Log("Found Animator component: " + _animator.name);
        }
        else
        {
            Debug.Log("Could not find Animator component");
        }
    }

    void FixedUpdate()
    {
        
        PositionMove();//player移动
        WalkAnimationController_K(); //判断player动画
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
    }




    private void WalkAnimationController_K() //这个是给主角判断动画的
    {
        if (!_animator)
        {
            return;
        }

        if (_moveInput != Vector3.zero)
        {
            PlayerManager.Instance.PlaySound("PlayerMove",transform);

            Random random = new Random();
            int _next=random.Next(0, 6);

            _audioSource.clip =FootAudioClips[_next];
            _audioSource.Play();
            
            if (_audioSource.isActiveAndEnabled == false)
            {
                _audioSource.enabled = true;
            }

            _animator.SetBool(IsRunning, true);
            _stopXk = _moveInput.x;
            _stopYk = _moveInput.y; //如果在移动就更新用于判断动画状态的xy
        }
        else
        {
            _audioSource.Pause();

            _animator.SetBool(IsRunning, false); //不在移动就不更新用于动画判断的xy
        }

        _animator.SetFloat(InputX, _stopXk);
        _animator.SetFloat(InputY, _stopYk);

       

    }

    void PositionMove()
    {
        var position = transform.position;

        position.x += _moveInput.x * speed * Time.deltaTime;
        position.z += _moveInput.y * speed * Time.deltaTime; //暂时是在zx平面上移动



        _playerRig.MovePosition(position);
        playerLocation = position;
    }

}