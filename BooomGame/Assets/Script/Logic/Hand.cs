using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Hand : MonoBehaviour
{
    
    public Animator _animator; // �������������
    public bool a = false;
    public bool b = false;

    private void Update()
    {
        if (a)
        {
            _animator.SetTrigger("Over");
            a = false;
        }

        if (b)
        {
            SceneManager.LoadScene(1);
        }
    }


    public void ReloadCurrentScene()
    {
        // ��ȡ��ǰ����������
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;


        // ���¼��ص�ǰ����
        SceneManager.LoadScene(currentSceneIndex);
    }

}
