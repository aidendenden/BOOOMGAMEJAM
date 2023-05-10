using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum StateType
{
    Idle,Walking,Chase
}
[Serializable]
public class Parameter
{
    public float moveSpeed;
    public float chaseSpeed;
    public float idleTime;

    public Transform[] WalkPoints;

    public Animator _animator;
}

public class FSMforSuGuan : MonoBehaviour
{
    public Parameter parameter;

    private IState currenState;

    private Dictionary<StateType, IState> states = new Dictionary<StateType, IState>();
    void Start()
    {
        states.Add(StateType.Idle, new IdleState(this));
        states.Add(StateType.Walking, new WalkState(this));
        states.Add(StateType.Chase, new ChaseState(this));

        TransitionState(StateType.Idle);




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
    }

    public void TransitionState(StateType type)
    {
        if(currenState != null)
        {
            currenState.OnExit();
        }
        currenState = states[type];
        currenState.OnEnter();

    }

}
