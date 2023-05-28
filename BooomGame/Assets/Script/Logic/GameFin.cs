using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFin : MonoBehaviour
{
    public GameObject ui;
    public Animator animator; // 渐变动画控制器组件
    public Animator _animator;//推镜头
    public bool a;


    private void Update()
    {
        if (a)
        {
            a = false;
            SceneManager.LoadScene(3);

        }
    }
    private void OnTriggerEnter(Collider other)
    {

        if(other.gameObject.tag == "Player")
        {
            ui.SetActive(true);
            animator.SetTrigger("OUT");
        }
    }
}
