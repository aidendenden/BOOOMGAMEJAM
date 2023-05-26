using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    
    public Animator _animator; // 动画控制器组件
    public bool a = false;

    private void Update()
    {
        if (a)
        {
            _animator.SetTrigger("Over");
            a = false;
        }
    }

}
