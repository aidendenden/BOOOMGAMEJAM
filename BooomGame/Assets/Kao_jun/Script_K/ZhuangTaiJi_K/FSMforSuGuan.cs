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

    public Transform SourceOfSound;

    public float distance;
    public float BiaoZhunDistance = 2;

    public  Transform Self;

  


}

public class FSMforSuGuan : MonoBehaviour
{
  

    public Parameter parameter;

    private IState currenState;

 

    private Dictionary<StateType, IState> states = new Dictionary<StateType, IState>();
    void Start()
    {
        GameManager.AlertnessValue = 0;
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

        if (transform.TryGetComponent<Transform>(out parameter.Self))
        {
            Debug.Log("Found Transform component: " + parameter.Self.name);
        }
        else
        {
            Debug.Log("Could not find Transform component");

        }



        PlayerEventManager.Instance.AddListener(delegate(string message, StuffEnum item, TriggerType type,Transform _transform)
        {
            if (message =="AlertnessValueHasChange")
            {
                parameter.SourceOfSound = _transform;
               
            }
        });
        
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
        if (GameManager.AlertnessValue >= GameManager.AlertnessMax)
        {
           
            parameter.chaseTarget = parameter.SourceOfSound;//������������ˣ�׷��Ŀ�������Դ
            GameManager.AlertnessValue -= GameManager.AlertnessDownSpeed * Time.deltaTime;
        }
        else
        {
            parameter.chaseTarget = null;
            GameManager.AlertnessValue -= GameManager.AlertnessDownSpeed * Time.deltaTime;
            if (GameManager.AlertnessValue <= 0)
            {
                GameManager.AlertnessValue = 0;
            }

        }
    }

    

   

}
