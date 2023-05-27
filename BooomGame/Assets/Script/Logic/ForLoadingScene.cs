using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ForLoadingScene : MonoBehaviour
{
    public float delayTime = 8f; // 延迟的时间，单位为秒

    private void Start()
    {
        // 在指定的延迟时间后调用函数
        Invoke("DelayedFunction", delayTime);
    }

    private void DelayedFunction()
    {
        // 在这里编写需要延迟执行的函数的逻辑
        SceneManager.LoadScene(2);
    }
}
