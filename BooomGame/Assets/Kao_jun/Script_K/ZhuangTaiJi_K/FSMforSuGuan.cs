using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public enum StateType
{
    Idle,Walking,Chase//有待机，巡逻，追击这三个状态
}
[Serializable]
public class Parameter
{
    public float moveSpeed;//巡逻时的速度
    public float idleTime;//每次待机的时长

    public Transform[] WalkPoints;//保存巡逻点的数组

    public Animator _animator;

    public Transform chaseTarget;//追击的目标

    public NavMeshAgent naw;

    public float watchfulnessMax = 100;//警觉度上限
    public float watchfulnessNow = 0;
    public float watchfulnessDownSpeed = 1;//警觉度下降速度
}

public class FSMforSuGuan : MonoBehaviour
{
    public Parameter parameter;

    private IState currenState;

    public Transform player;


    

    private Dictionary<StateType, IState> states = new Dictionary<StateType, IState>();
    void Start()
    {
        //注册状态
        states.Add(StateType.Idle, new IdleState(this));
        states.Add(StateType.Walking, new WalkState(this));
        states.Add(StateType.Chase, new ChaseState(this));



        TransitionState(StateType.Idle);//进入待机状态



        //获取动画组件
        if (transform.TryGetComponent<Animator>(out parameter._animator))
        {
            Debug.Log("Found Animator component: " + parameter._animator.name);
        }
        else
        {
            Debug.Log("Could not find Animator component");

        }
        

    }

   
    void  FixedUpdate()
    {
        
        
        currenState.OnUpdate();
        CheckWatchfulness();//检查当前警觉度



    }

    public void TransitionState(StateType type)//改变状态
    {
        if(currenState != null)
        {
            currenState.OnExit();
        }
        currenState = states[type];
        currenState.OnEnter();

    }




    void CheckWatchfulness()//警觉度检查
    {
       // Debug.Log("当前警戒指数" + parameter.watchfulnessNow);
        if (parameter.watchfulnessNow >= parameter.watchfulnessMax)
        {
            parameter.chaseTarget = player;//如果警觉度满了，追击目标就是玩家
        }
        else
        {
            parameter.chaseTarget = null;
            parameter.watchfulnessNow -= parameter.watchfulnessDownSpeed * Time.deltaTime;
            if (parameter.watchfulnessNow <= 0)
            {
                parameter.watchfulnessNow = 0;
            }

        }
    }
    

}
