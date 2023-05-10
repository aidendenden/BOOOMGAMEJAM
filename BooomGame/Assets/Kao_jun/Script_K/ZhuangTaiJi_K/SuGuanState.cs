using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }


    public void OnExit()
    {
        timer = 0;
    }
}




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

    }

    public void OnExit()
    {

    }

    public void OnUpdate()
    {

    }
}
