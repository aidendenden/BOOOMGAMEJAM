using UnityEngine;

public class ActivateSpriteOnTrigger : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public bool IsAtive = false;


    private void Start()
    {
        // ��ȡ��ǰ�����ϵ� SpriteRenderer ���
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
        IsAtive = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        // �����봥�����Ķ����Ƿ������
        if (other.CompareTag("Player"))
        {
            // ���� SpriteRenderer ���
            spriteRenderer.enabled = true;
            IsAtive = true;
         
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // ���� SpriteRenderer ���
            spriteRenderer.enabled = false;
            IsAtive = false;

        }
    }
}
