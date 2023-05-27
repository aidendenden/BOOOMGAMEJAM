using UnityEngine;

public class ActivateSpriteOnTrigger : MonoBehaviour
{
    
    public bool IsAtive = false;
    public Animator animator;

    private void Start()
    {
        // 获取当前对象上的 SpriteRenderer 组件
        //进入碰撞箱之后激活子物体
       
       
        IsAtive = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        // 检查进入触发器的对象是否是玩家
        if (other.CompareTag("Player")&&IsAtive==false)
        {
            // 激活 SpriteRenderer 组件
            animator.SetTrigger("SW");
            IsAtive = true;
         
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")&&IsAtive==true)
        {
            // 激活 SpriteRenderer 组件
            animator.SetTrigger("SW");
            IsAtive = false;

        }
    }
}
