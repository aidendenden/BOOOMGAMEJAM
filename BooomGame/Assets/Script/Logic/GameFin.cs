using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFin : MonoBehaviour
{
    public Animator animator; // �������������
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
            animator.SetTrigger("OUT");
        }
    }
}