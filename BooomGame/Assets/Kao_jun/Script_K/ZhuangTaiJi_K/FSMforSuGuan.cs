using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public enum StateType
{
    Idle,Walking,Chase//�д�����Ѳ�ߣ�׷��������״̬
}
[Serializable]
public class Parameter
{
    public float moveSpeed;//Ѳ��ʱ���ٶ�
    public float idleTime;//ÿ�δ�����ʱ��

    public Transform[] WalkPoints;//����Ѳ�ߵ������

    public Animator _animator;

    public Transform chaseTarget;//׷����Ŀ��

    public NavMeshAgent naw;

    public float watchfulnessMax = 100;//����������
    public float watchfulnessNow = 0;
    public float watchfulnessDownSpeed = 1;//�������½��ٶ�
}

public class FSMforSuGuan : MonoBehaviour
{
    public Parameter parameter;

    private IState currenState;

    public Transform player;


    

    private Dictionary<StateType, IState> states = new Dictionary<StateType, IState>();
    void Start()
    {
        //ע��״̬
        states.Add(StateType.Idle, new IdleState(this));
        states.Add(StateType.Walking, new WalkState(this));
        states.Add(StateType.Chase, new ChaseState(this));



        TransitionState(StateType.Idle);//�������״̬



        //��ȡ�������
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
        CheckWatchfulness();//��鵱ǰ������



    }

    public void TransitionState(StateType type)//�ı�״̬
    {
        if(currenState != null)
        {
            currenState.OnExit();
        }
        currenState = states[type];
        currenState.OnEnter();

    }




    void CheckWatchfulness()//�����ȼ��
    {
       // Debug.Log("��ǰ����ָ��" + parameter.watchfulnessNow);
        if (parameter.watchfulnessNow >= parameter.watchfulnessMax)
        {
            parameter.chaseTarget = player;//������������ˣ�׷��Ŀ��������
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
