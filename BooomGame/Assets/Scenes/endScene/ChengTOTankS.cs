using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChengTOTankS : MonoBehaviour
{
    private Animator animator; // 渐变动画控制器组件
    public GameObject image0;
    public GameObject image1;

    public bool a;
    public bool b;
    
    private void Start()
    {
        if (GameManager.Instance.isGetDogBone)
        {
            image0.SetActive(false);
            image1.SetActive(true);

            animator = image1.GetComponent<Animator>();
        }
        else
        {
            image0.SetActive(true);
            image1.SetActive(false);

            animator = image0.GetComponent<Animator>();
        }
    }

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