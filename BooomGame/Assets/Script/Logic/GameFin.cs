using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFin : MonoBehaviour
{
    public GameObject ui;
    public Animator animator; // 动画控制器组件
    public bool a;


    private void Update()
    {
        if (a)
        {
            a = false;
            SceneManager.LoadScene(0);

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
