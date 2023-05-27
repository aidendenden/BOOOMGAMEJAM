using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Hand : MonoBehaviour
{
    
    public Animator _animator; // 动画控制器组件
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
        // 获取当前场景的索引
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;


        // 重新加载当前场景
        SceneManager.LoadScene(currentSceneIndex);
    }

}
