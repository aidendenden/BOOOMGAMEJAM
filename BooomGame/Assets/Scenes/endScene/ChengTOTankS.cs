using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChengTOTankS : MonoBehaviour
{
    
    public Animator animator; // 渐变动画控制器组件

    public bool a;
    public bool b;


    private void Update()
    {
        if (a)
        {
            a = false;
            SceneManager.LoadScene(2);

        }
        if (b)
        {
            b = false;
            animator.SetTrigger("fin");
        }
    }
    
}
