using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// 这里是待机状态
/// </summary>
public class IdleState : IState
{
    private FSMforSuGuan manager;

    private Parameter _parameter;


    private float timer;

    public IdleState(FSMforSuGuan manager)
    {
        this.manager = manager;
        this._parameter = manager.parameter;
    }
    public void OnEnter()
    {
        _parameter._animator.Play("KplayerIdieF");
    }

    public void OnUpdate()
    {
        timer += Time.deltaTime;

        if(timer >= _parameter.idleTime)
        {
            manager.TransitionState(StateType.Walking);
        }

        if(_parameter.chaseTarget != null)//如果有追击目标就进入追击状态
        {
            manager.TransitionState(StateType.Chase);
        }
    }

    public void OnExit()
    {
        timer = 0;
    }
}



/// <summary>
/// 这里是巡逻状态
/// </summary>
public class WalkState : IState
{
    private FSMforSuGuan manager;

    private Parameter _parameter;

    private int walkPosition;

    public WalkState(FSMforSuGuan manager)
    {
        this.manager = manager;
        this._parameter = manager.parameter;
    }
    public void OnEnter()
    {
        _parameter._animator.Play("KplayerRunF");
    }

    public void OnUpdate()
    {
        manager.transform.position = Vector3.MoveTowards(manager.transform.position,_parameter.WalkPoints[walkPosition].position, _parameter.moveSpeed * Time.deltaTime);

        if (Vector3.Distance(manager.transform.position, _parameter.WalkPoints[walkPosition].position) < .1f){

            manager.TransitionState(StateType.Idle);
        }

        if (_parameter.chaseTarget != null)//如果有追击目标就进入追击状态
        {
            manager.TransitionState(StateType.Chase);
        }
    }

    public void OnExit()
    {
        walkPosition++;

            if(walkPosition >= _parameter.WalkPoints.Length)
        {
            walkPosition = 0;
        }
    }
}



/// <summary>
/// 这里是追击状态
/// </summary>
public class ChaseState : IState
{
    private FSMforSuGuan manager;

    private Parameter _parameter;
    


    public ChaseState(FSMforSuGuan manager)
    {
        this.manager = manager;
        this._parameter = manager.parameter;
    }
    public void OnEnter()
    {
        _parameter._animator.Play("KplayerRunF");
    }


    public void OnUpdate()
    {
        if (_parameter.chaseTarget !=null)
        {
            _parameter.naw.SetDestination(_parameter.chaseTarget.position);
        }

        if(_parameter.chaseTarget == null)
        {
            manager.TransitionState(StateType.Idle);
        }
    }


    public void OnExit()
    {

    }

   
}
