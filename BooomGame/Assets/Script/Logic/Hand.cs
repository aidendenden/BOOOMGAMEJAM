using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    
    public Animator _animator; // �������������
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
