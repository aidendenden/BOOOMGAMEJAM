using UnityEngine;

public class ActivateSpriteOnTrigger : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public bool IsAtive = false;


    private void Start()
    {
        // 获取当前对象上的 SpriteRenderer 组件
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
        IsAtive = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        // 检查进入触发器的对象是否是玩家
        if (other.CompareTag("Player"))
        {
            // 激活 SpriteRenderer 组件
            spriteRenderer.enabled = true;
            IsAtive = true;
         
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // 激活 SpriteRenderer 组件
            spriteRenderer.enabled = false;
            IsAtive = false;

        }
    }
}
