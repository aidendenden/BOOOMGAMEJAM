using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ForLoadingScene : MonoBehaviour
{
    public float delayTime = 8f; // �ӳٵ�ʱ�䣬��λΪ��

    private void Start()
    {
        // ��ָ�����ӳ�ʱ�����ú���
        Invoke("DelayedFunction", delayTime);
    }

    private void DelayedFunction()
    {
        // �������д��Ҫ�ӳ�ִ�еĺ������߼�
        SceneManager.LoadScene(2);
    }
}
