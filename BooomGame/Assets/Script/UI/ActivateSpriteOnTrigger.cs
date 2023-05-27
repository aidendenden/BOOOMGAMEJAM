using UnityEngine;

public class ActivateSpriteOnTrigger : MonoBehaviour
{
    
    public bool IsAtive = false;
    public Animator animator;

    private void Start()
    {
        // ��ȡ��ǰ�����ϵ� SpriteRenderer ���
        //������ײ��֮�󼤻�������
       
       
        IsAtive = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        // �����봥�����Ķ����Ƿ������
        if (other.CompareTag("Player")&&IsAtive==false)
        {
            // ���� SpriteRenderer ���
            animator.SetTrigger("SW");
            IsAtive = true;
         
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")&&IsAtive==true)
        {
            // ���� SpriteRenderer ���
            animator.SetTrigger("SW");
            IsAtive = false;

        }
    }
}
