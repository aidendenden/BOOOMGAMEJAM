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
            SceneManager.LoadScene(0);
        }
    }
 
}
