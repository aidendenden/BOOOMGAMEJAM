using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using Random =  UnityEngine.Random;

public class GameInputManage : MonoBehaviour
{
    private static readonly Lazy<GameInputManage> Lazy = new Lazy<GameInputManage>(() => new GameInputManage());

    private GameInputManage()
    {
    }

    public static GameInputManage Instance => Lazy.Value;

    [HideInInspector] public Vector3 playerLocation;
    public bool isCanMove = true;
    public float speed = 5f;
    private float speedBasic = 5f;//储存玩家初始的速度
    public float stopDrag = 50f;//用于在物理运动中实现缓停
    public List<AudioClip> FootAudioClips;

    public Animator Hand_animator; // 黑手的动画控制器组件
    

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
        speedBasic = speed;

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
        isCanMove = GameManager.IsInteracting == 0;
        if (isCanMove)
        {
            SpeedUP();//检测是否按下加速键加速
            PositionMove();//player移动
            WalkAnimationController_K(); //判断player动画
        }

        
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
            PlayerEventManager.Instance.Triggered("PlayerMove",StuffEnum.Null,TriggerType.Null,transform);

            if (_audioSource.isPlaying == false)
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    _audioSource.clip = FootAudioClips[Random.Range(4, 6)];
                    _audioSource.PlayOneShot(_audioSource.clip);
                }
                else
                {
                    _audioSource.clip = FootAudioClips[Random.Range(0, 4)];
                    _audioSource.PlayOneShot(_audioSource.clip);
                }
            };
            
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

    void SpeedUP()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = speedBasic + 4;
           // PlayerEnd();
        }
        else
        {
            speed = speedBasic;
        }
    }


    void PlayerEnd()
    {

        isCanMove = false;

        Hand_animator.SetTrigger("Over");
       
    }


}